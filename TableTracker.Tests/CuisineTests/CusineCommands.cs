using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableTracker.Domain.Interfaces.Repositories;
using TableTracker.Domain.Interfaces;
using Xunit;
using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using AutoMapper;
using TableTracker.Application.CQRS.Cuisines.Commands.AddCuisine;
using System.Threading;
using TableTracker.Domain.Enums;
using TableTracker.Application.CQRS.Cuisines.Commands.DeleteCuisine;
using TableTracker.Application.CQRS.Cuisines.Commands.UpdateCuisine;
using System.Diagnostics.CodeAnalysis;

namespace TableTracker.Tests.CuisineTests
{
    public class CusineCommands
    {
        private Mock<IUnitOfWork<long>> unitOfWorkMock;
        private Mock<ICuisineRepository> _cuisineRepositorн;

        public CusineCommands()
        {
            unitOfWorkMock = new Mock<IUnitOfWork<long>>();
            _cuisineRepositorн = new Mock<ICuisineRepository>();

            unitOfWorkMock
                .Setup(uow => uow.GetRepository<ICuisineRepository>())
                .Returns(_cuisineRepositorн.Object);
        }

        [Fact]
        public async void AddCuisine()
        {
            var cuisine = new Cuisine { CuisineName = "Ukrainian" };
            var cuisineDto = new CuisineDTO { Cuisine = "Ukrainian" };

            _cuisineRepositorн
                .Setup(repo => repo.Insert(It.IsAny<Cuisine>()))
                .Returns(Task.CompletedTask);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<Cuisine>(It.IsAny<CuisineDTO>())).Returns(cuisine);
            mapperMock
                .Setup(mapper => mapper.Map<CuisineDTO>(It.IsAny<Cuisine>()))
                .Returns((Cuisine c) => new CuisineDTO { Cuisine = c.CuisineName});

            var command = new AddCuisineCommand(cuisineDto);

            var handler = new AddCuisineCommandHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(CommandResult.Success, result.Result);
            Assert.Equal(cuisineDto.Cuisine, result.Object.Cuisine);
        }

        [Fact]
        public async void DeleteCuisine()
        {
            var cuisine = new Cuisine { CuisineName = "Ukrainian", Id = 1 };

            _cuisineRepositorн
                 .Setup(repo => repo.FindById(1))
                 .ReturnsAsync(cuisine);

            _cuisineRepositorн.Setup(r => r.Contains(cuisine))
                .ReturnsAsync(true);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<CuisineDTO>(cuisine))
                .Returns(new CuisineDTO { Id = cuisine.Id, Cuisine = cuisine.CuisineName });

            var command = new DeleteCuisineCommand(1);

            var handler = new DeleteCuisineCommandHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(CommandResult.Success, result.Result);
            Assert.Equal(cuisine.Id, result.Object.Id);
        }

        [Fact]
        public async void UpdateCuisine()
        {
            var cuisine = new Cuisine { CuisineName = "Ukrainian", Id = 1 };
            var cuisineDto = new CuisineDTO { Cuisine = "Ukrainian", Id = 1 };

            _cuisineRepositorн.Setup(r => r.Contains(It.IsAny<Cuisine>()))
                .ReturnsAsync(true);

            _cuisineRepositorн.Setup(r => r.Update(It.IsAny<Cuisine>()));

            _cuisineRepositorн.Setup(r => r.FindById(1))
                .ReturnsAsync(cuisine);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<Cuisine>(It.IsAny<CuisineDTO>()))
                .Returns(cuisine);
            mapperMock.Setup(mapper => mapper.Map<CuisineDTO>(It.IsAny<Cuisine>()))
                .Returns(cuisineDto);

            var command = new UpdateCuisineCommand(cuisineDto);

            var handler = new UpdateCuisineCommandHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            _cuisineRepositorн.Verify(repo => repo.Contains(cuisine), Times.Once);
            _cuisineRepositorн.Verify(repo => repo.Update(cuisine), Times.Once);
            Assert.Equal(CommandResult.Success, result.Result);
            Assert.Equal(cuisineDto.Cuisine, result.Object.Cuisine);

        }

    }
}
