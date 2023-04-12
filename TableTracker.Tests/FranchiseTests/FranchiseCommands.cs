using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableTracker.Domain.Interfaces.Repositories;
using TableTracker.Domain.Interfaces;
using Xunit;
using AutoMapper;
using System.Threading;
using TableTracker.Application.CQRS.Cuisines.Commands.AddCuisine;
using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Domain.Enums;
using TableTracker.Infrastructure.Repositories;
using TableTracker.Application.CQRS.Franchises.Commands.AddFranchise;
using TableTracker.Application.CQRS.Cuisines.Commands.DeleteCuisine;
using TableTracker.Application.CQRS.Franchises.Commands.DeleteFranchise;
using TableTracker.Application.CQRS.Cuisines.Commands.UpdateCuisine;
using TableTracker.Application.CQRS.Franchises.Commands.UpdateFranchise;

namespace TableTracker.Tests.FranchiseTests
{
    public class FranchiseCommands
    {
        private Mock<IUnitOfWork<long>> unitOfWorkMock;
        private Mock<IFranchiseRepository> _franchiseRepository;

        public FranchiseCommands()
        {
            unitOfWorkMock = new Mock<IUnitOfWork<long>>();
            _franchiseRepository = new Mock<IFranchiseRepository>();

            unitOfWorkMock
                .Setup(uow => uow.GetRepository<IFranchiseRepository>())
                .Returns(_franchiseRepository.Object);
        }

        [Fact]
        public async void AddFranchise()
        {
            var franchise = new Franchise { Name = "McDonalds" };
            var franchiseDto = new FranchiseDTO { Name = "McDonalds" };

            _franchiseRepository
                .Setup(repo => repo.Insert(It.IsAny<Franchise>()))
                .Returns(Task.CompletedTask);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<Franchise>(It.IsAny<FranchiseDTO>())).Returns(franchise);
            mapperMock
                .Setup(mapper => mapper.Map<FranchiseDTO>(It.IsAny<Franchise>()))
                .Returns((Franchise c) => new FranchiseDTO { Name = c.Name });

            var command = new AddFranchiseCommand(franchiseDto);

            var handler = new AddFranchiseCommandHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(CommandResult.Success, result.Result);
            Assert.Equal(franchiseDto.Name, result.Object.Name);
        }

        [Fact]
        public async void DeleteFranchise()
        {
            var franchise = new Franchise { Name = "McDonalds", Id = 1 };

            _franchiseRepository
                 .Setup(repo => repo.FindById(1))
                 .ReturnsAsync(franchise);

            _franchiseRepository.Setup(r => r.Contains(franchise))
                .ReturnsAsync(true);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<FranchiseDTO>(franchise))
                .Returns(new FranchiseDTO { Id = franchise.Id, Name = franchise.Name });

            var command = new DeleteFranchiseCommand(1);

            var handler = new DeleteFranchiseCommandHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(CommandResult.Success, result.Result);
            Assert.Equal(franchise.Id, result.Object.Id);
            Assert.Equal(franchise.Name, result.Object.Name);
        }

        [Fact]
        public async void UpdateFranchise()
        {
            var franchise = new Franchise { Name = "McDonalds" };
            var franchiseDto = new FranchiseDTO { Name = "McDonalds" };

            _franchiseRepository.Setup(r => r.Contains(It.IsAny<Franchise>()))
                .ReturnsAsync(true);

            _franchiseRepository.Setup(r => r.Update(It.IsAny<Franchise>()));

            _franchiseRepository.Setup(r => r.FindById(1))
                .ReturnsAsync(franchise);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<Franchise>(It.IsAny<FranchiseDTO>()))
                .Returns(franchise);
            mapperMock.Setup(mapper => mapper.Map<FranchiseDTO>(It.IsAny<Franchise>()))
                .Returns(franchiseDto);

            var command = new UpdateFranchiseCommand(franchiseDto);

            var handler = new UpdateFranchiseCommandHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            _franchiseRepository.Verify(repo => repo.Contains(franchise), Times.Once);
            _franchiseRepository.Verify(repo => repo.Update(franchise), Times.Once);
            Assert.Equal(CommandResult.Success, result.Result);
            Assert.Equal(franchiseDto.Name, result.Object.Name);
        }

    }
}
