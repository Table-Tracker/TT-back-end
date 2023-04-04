using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableTracker.Domain.Interfaces.Repositories;
using TableTracker.Domain.Interfaces;
using Xunit;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using System.Threading;
using TableTracker.Application.CQRS.Managers.Queries.FindManagerById;
using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Infrastructure.Repositories;
using TableTracker.Application.CQRS.Reservations.Queries.FindReservationById;
using TableTracker.Application.CQRS.Managers.Queries.GetAllManagers;
using TableTracker.Application.CQRS.Reservations.Queries.GetAllReservations;
using TableTracker.Application.CQRS.Reservations.Queries.GetAllReservationsByDate;
using TableTracker.Application.CQRS.Reservations.Queries.GetAllReservationsByDateAndTime;

namespace TableTracker.Tests.ReservationTests
{
    public class ReservationQueries
    {
        private Mock<IUnitOfWork<long>> unitOfWorkMock;
        private Mock<IReservationRepository> _reservationRepository;

        public ReservationQueries()
        {
            unitOfWorkMock = new Mock<IUnitOfWork<long>>();
            _reservationRepository = new Mock<IReservationRepository>();

            unitOfWorkMock
                .Setup(uow => uow.GetRepository<IReservationRepository>())
                .Returns(_reservationRepository.Object);
        }

        [Fact]
        public async void FindReservationById()
        {
            var query = new FindReservationByIdQuery(1);

            var reservation = new Reservation { Id = 1 };
            var reservationDTO = new ReservationDTO { Id = 1 };

            _reservationRepository
                .Setup(repo => repo.FindById(1))
                .ReturnsAsync(reservation);

            var mapperMock = new Mock<IMapper>();
            mapperMock
                .Setup(mapper => mapper.Map<ReservationDTO>(reservation))
                .Returns(reservationDTO);

            var handler = new FindReservationsByIdQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(reservation.Id, result.Id);
        }

        [Fact]
        public async void GetAllReservations()
        {
            var restaurant = new Restaurant { Id = 1, Email = "LMAO@g.com" };
            var resDto = new RestaurantDTO { Id = 1, Email = "LMAO@g.com" };

            var tables = new List<Table>()
            {
                new Table { RestaurantId = 1, Restaurant = restaurant, Id = 1 },
                new Table { RestaurantId = 1, Restaurant = restaurant, Id = 2 },
                new Table { RestaurantId = 1, Restaurant = restaurant, Id = 3 },
                new Table { RestaurantId = 1, Restaurant = restaurant, Id = 4 },
                new Table { RestaurantId = 1, Restaurant = restaurant, Id = 5 },
            };
            var tablesDTO = new List<TableDTO>()
            {
                new TableDTO { Restaurant = resDto, Id = 1 },
                new TableDTO { Restaurant = resDto, Id = 2 },
                new TableDTO { Restaurant = resDto, Id = 3 },
                new TableDTO { Restaurant = resDto, Id = 4 },
                new TableDTO { Restaurant = resDto, Id = 5 },
            };
            var reservations = new List<Reservation>()
            {
                new Reservation { Id = 1, Table = tables[0], TableId = 1 },
                new Reservation { Id = 2, Table = tables[1], TableId = 2 },
                new Reservation { Id = 3, Table = tables[2], TableId = 3 },
                new Reservation { Id = 4, Table = tables[3], TableId = 4 },
                new Reservation { Id = 5, Table = tables[4], TableId = 5 },
            };
            var reservationsDTO = new List<ReservationDTO>()
            {
                new ReservationDTO { Id = 1, Table = tablesDTO[0] },
                new ReservationDTO { Id = 2, Table = tablesDTO[1] },
                new ReservationDTO { Id = 3, Table = tablesDTO[2] },
                new ReservationDTO { Id = 4, Table = tablesDTO[3] },
                new ReservationDTO { Id = 5, Table = tablesDTO[4] },
            };

            var query = new GetAllReservationsQuery(1);

            _reservationRepository
                .Setup(repo => repo.GetAllReservations(It.IsAny<long>()))
                .ReturnsAsync(reservations);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<ReservationDTO>(It.IsAny<Reservation>()))
                 .Returns((Reservation c) => new ReservationDTO
                 {
                     Id = c.Id,
                 });

            var handler = new GetAllRevervationsQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            var check = true;

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i].Id != reservationsDTO[i].Id)
                {
                    check = false;
                    break;
                }
            }

            Assert.True(check);
        }

        [Fact]
        public async void GetAllReservationsByDate()
        {
            var restaurant = new Restaurant { Id = 1, Email = "LMAO@g.com" };
            var resDto = new RestaurantDTO { Id = 1, Email = "LMAO@g.com" };

            var tables = new List<Table>()
            {
                new Table { RestaurantId = 1, Restaurant = restaurant, Id = 1 },
                new Table { RestaurantId = 1, Restaurant = restaurant, Id = 2 },
                new Table { RestaurantId = 1, Restaurant = restaurant, Id = 3 },
                new Table { RestaurantId = 1, Restaurant = restaurant, Id = 4 },
                new Table { RestaurantId = 1, Restaurant = restaurant, Id = 5 },
            };
            var tablesDTO = new List<TableDTO>()
            {
                new TableDTO { Restaurant = resDto, Id = 1 },
                new TableDTO { Restaurant = resDto, Id = 2 },
                new TableDTO { Restaurant = resDto, Id = 3 },
                new TableDTO { Restaurant = resDto, Id = 4 },
                new TableDTO { Restaurant = resDto, Id = 5 },
            };
            var reservations = new List<Reservation>()
            {
                new Reservation { Id = 1, Table = tables[0], TableId = 1, Date = DateTime.Today },
                new Reservation { Id = 2, Table = tables[1], TableId = 2, Date = DateTime.Today },
                new Reservation { Id = 3, Table = tables[2], TableId = 3, Date = DateTime.Today },
                new Reservation { Id = 4, Table = tables[3], TableId = 4, Date = DateTime.Today },
                new Reservation { Id = 5, Table = tables[4], TableId = 5, Date = DateTime.Today },
            };
            var reservationsDTO = new List<ReservationDTO>()
            {
                new ReservationDTO { Id = 1, Table = tablesDTO[0], Date = DateTime.Today },
                new ReservationDTO { Id = 2, Table = tablesDTO[1], Date = DateTime.Today },
                new ReservationDTO { Id = 3, Table = tablesDTO[2], Date = DateTime.Today },
                new ReservationDTO { Id = 4, Table = tablesDTO[3], Date = DateTime.Today },
                new ReservationDTO { Id = 5, Table = tablesDTO[4], Date = DateTime.Today },
            };

            var query = new GetAllReservationsByDateQuery(1, DateTime.Today);

            _reservationRepository
                .Setup(repo => repo.GetAllReservationsByDate(It.IsAny<long>(), DateTime.Today))
                .ReturnsAsync(reservations);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<ReservationDTO>(It.IsAny<Reservation>()))
                 .Returns((Reservation c) => new ReservationDTO
                 {
                     Id = c.Id,
                     Date= c.Date,
                 });

            var handler = new GetAllReservationsByDateQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            var check = true;

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i].Id != reservationsDTO[i].Id || result[i].Date != reservationsDTO[i].Date)
                {
                    check = false;
                    break;
                }
            }

            Assert.True(check);
        }

        [Fact]
        public async void GetAllReservationsByDateAndTime()
        {
            var restaurant = new Restaurant { Id = 1, Email = "LMAO@g.com" };
            var resDto = new RestaurantDTO { Id = 1, Email = "LMAO@g.com" };

            var tables = new List<Table>()
            {
                new Table { RestaurantId = 1, Restaurant = restaurant, Id = 1 },
                new Table { RestaurantId = 1, Restaurant = restaurant, Id = 2 },
                new Table { RestaurantId = 1, Restaurant = restaurant, Id = 3 },
                new Table { RestaurantId = 1, Restaurant = restaurant, Id = 4 },
                new Table { RestaurantId = 1, Restaurant = restaurant, Id = 5 },
            };
            var tablesDTO = new List<TableDTO>()
            {
                new TableDTO { Restaurant = resDto, Id = 1 },
                new TableDTO { Restaurant = resDto, Id = 2 },
                new TableDTO { Restaurant = resDto, Id = 3 },
                new TableDTO { Restaurant = resDto, Id = 4 },
                new TableDTO { Restaurant = resDto, Id = 5 },
            };
            var reservations = new List<Reservation>()
            {
                new Reservation { Id = 1, Table = tables[0], TableId = 1, Date = DateTime.Today },
                new Reservation { Id = 2, Table = tables[1], TableId = 2, Date = DateTime.Today },
                new Reservation { Id = 3, Table = tables[2], TableId = 3, Date = DateTime.Today },
                new Reservation { Id = 4, Table = tables[3], TableId = 4, Date = DateTime.Today },
                new Reservation { Id = 5, Table = tables[4], TableId = 5, Date = DateTime.Today },
            };
            var reservationsDTO = new List<ReservationDTO>()
            {
                new ReservationDTO { Id = 1, Table = tablesDTO[0], Date = DateTime.Today },
                new ReservationDTO { Id = 2, Table = tablesDTO[1], Date = DateTime.Today },
                new ReservationDTO { Id = 3, Table = tablesDTO[2], Date = DateTime.Today },
                new ReservationDTO { Id = 4, Table = tablesDTO[3], Date = DateTime.Today },
                new ReservationDTO { Id = 5, Table = tablesDTO[4], Date = DateTime.Today },
            };

            var query = new GetAllReservationsByDateAndTimeQuery(1, DateTime.Today);

            _reservationRepository
                .Setup(repo => repo.GetAllReservationsByDateAndTime(It.IsAny<long>(), DateTime.Today))
                .ReturnsAsync(reservations);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<ReservationDTO>(It.IsAny<Reservation>()))
                 .Returns((Reservation c) => new ReservationDTO
                 {
                     Id = c.Id,
                     Date = c.Date,
                 });

            var handler = new GetAllReservationsByDateQueryAndTimeHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            var check = true;

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i].Id != reservationsDTO[i].Id || result[i].Date != reservationsDTO[i].Date)
                {
                    check = false;
                    break;
                }
            }

            Assert.True(check);
        }

    }
}
