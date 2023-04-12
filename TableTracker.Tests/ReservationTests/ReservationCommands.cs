using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableTracker.Domain.Interfaces.Repositories;
using TableTracker.Domain.Interfaces;
using AutoMapper;
using System.Threading;
using TableTracker.Application.CQRS.Managers.Commands.AddManager;
using TableTracker.Application.CQRS.Managers.Commands.DeleteManager;
using TableTracker.Application.CQRS.Managers.Commands.UpdateManager;
using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Domain.Enums;
using TableTracker.Infrastructure.Repositories;
using Xunit;
using TableTracker.Application.CQRS.Reservations.Commands.AddReservation;
using TableTracker.Application.CQRS.Reservations.Commands.DeleteReservation;
using TableTracker.Application.CQRS.Reservations.Commands.UpdateReservation;

namespace TableTracker.Tests.ReservationTests
{
    public class ReservationCommands
    {
        private Mock<IUnitOfWork<long>> unitOfWorkMock;
        private Mock<IReservationRepository> _reservationRepository;

        public ReservationCommands()
        {
            unitOfWorkMock = new Mock<IUnitOfWork<long>>();
            _reservationRepository = new Mock<IReservationRepository>();

            unitOfWorkMock
                .Setup(uow => uow.GetRepository<IReservationRepository>())
                .Returns(_reservationRepository.Object);
        }

        [Fact]
        public async void AddReservation()
        {
            var reservation = new Reservation { Date = DateTime.Today };
            var reservationDTO = new ReservationDTO { Date = DateTime.Today };

            _reservationRepository
                .Setup(repo => repo.Insert(It.IsAny<Reservation>()))
                .Returns(Task.CompletedTask);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<Reservation>(It.IsAny<ReservationDTO>())).Returns(reservation);
            mapperMock
                .Setup(mapper => mapper.Map<ReservationDTO>(It.IsAny<Reservation>()))
                .Returns((Reservation c) => new ReservationDTO { Date = c.Date });

            var command = new AddReservationCommand(reservationDTO);

            var handler = new AddReservationCommandHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(CommandResult.Success, result.Result);
            Assert.Equal(reservationDTO.Date, result.Object.Date);
        }

        [Fact]
        public async void DeleteReservation()
        {
            var reservation = new Reservation { Date = DateTime.Today, Id = 1 };

            _reservationRepository
                 .Setup(repo => repo.FindById(1))
                 .ReturnsAsync(reservation);

            _reservationRepository.Setup(r => r.Contains(reservation))
                .ReturnsAsync(true);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<ReservationDTO>(reservation))
                .Returns(new ReservationDTO { Id = reservation.Id, Date = reservation.Date });

            var command = new DeleteReservationCommand(1);

            var handler = new DeleteReservationCommandHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(CommandResult.Success, result.Result);
            Assert.Equal(reservation.Id, result.Object.Id);
            Assert.Equal(reservation.Date, result.Object.Date);
        }

        [Fact]
        public async void UpdateReservation()
        {
            var reservation = new Reservation { Date = DateTime.Today };
            var reservationDTO = new ReservationDTO { Date = DateTime.Today };

            _reservationRepository.Setup(r => r.Contains(It.IsAny<Reservation>()))
                .ReturnsAsync(true);

            _reservationRepository.Setup(r => r.Update(It.IsAny<Reservation>()));

            _reservationRepository.Setup(r => r.FindById(1))
                .ReturnsAsync(reservation);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<Reservation>(It.IsAny<ReservationDTO>()))
                .Returns(reservation);
            mapperMock.Setup(mapper => mapper.Map<ReservationDTO>(It.IsAny<Reservation>()))
            .Returns(reservationDTO);

            var command = new UpdateReservationCommand(reservationDTO);

            var handler = new UpdateReservationCommandHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            _reservationRepository.Verify(repo => repo.Contains(reservation), Times.Once);
            _reservationRepository.Verify(repo => repo.Update(reservation), Times.Once);
            Assert.Equal(CommandResult.Success, result.Result);
            Assert.Equal(reservationDTO.Date, result.Object.Date);
        }
    }
}
