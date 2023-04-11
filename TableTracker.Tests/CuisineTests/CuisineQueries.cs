using AutoMapper;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TableTracker.Application.CQRS.Cuisines.Queries.FindCuisineById;
using TableTracker.Application.CQRS.Cuisines.Queries.GetAllCuisines;
using TableTracker.Application.CQRS.Cuisines.Queries.GetCuisineByName;
using TableTracker.Controllers;
using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;
using TableTracker.Tests.UnitOfWork;
using Xunit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TableTracker.Tests.CuisineTests
{
    public class CuisineQueries
    {
        private Mock<IUnitOfWork<long>> unitOfWorkMock;
        private Mock<ICuisineRepository> cuisineRepositoryMock;

        public CuisineQueries()
        {
            unitOfWorkMock = new Mock<IUnitOfWork<long>>();
            cuisineRepositoryMock = new Mock<ICuisineRepository>();

            unitOfWorkMock
                .Setup(uow => uow.GetRepository<ICuisineRepository>())
                .Returns(cuisineRepositoryMock.Object);
        }

        [Fact]
        public async void GetCuisineByName()
        {
            var cuisineName = "Ukrainian";

            //var entities = new List<Cuisine>()
            //{
            //    new Cuisine { CuisineName = "International" },
            //    new Cuisine { CuisineName = "Ukranian" },
            //    new Cuisine { CuisineName = "Polish" },
            //    new Cuisine { CuisineName = "English" },
            //    new Cuisine { CuisineName = "Italian" },
            //};

            var query = new GetCuisineByNameQuery("Ukrainian");


            var cuisine = new Cuisine { CuisineName = "Ukranian" };
            var cuisineDto = new CuisineDTO { Cuisine = "Ukrainian" };

            cuisineRepositoryMock
                .Setup(repo => repo.GetCuisineByName("Ukrainian"))
                .ReturnsAsync(cuisine);

            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(mapper => mapper.Map<CuisineDTO>(cuisine))
                .Returns(cuisineDto);

            var handler = new GetCuisineByNameQueryHandler(unitOfWorkMock.Object, mapperMock.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(cuisineDto.Cuisine, result.Cuisine);

        }

        [Fact]
        public async void GetAllCuisine()
        {
            var cuisines = new List<Cuisine>()
            {
                new Cuisine { CuisineName = "International" },
                new Cuisine { CuisineName = "Ukranian" },
                new Cuisine { CuisineName = "Polish" },
                new Cuisine { CuisineName = "English" },
                new Cuisine { CuisineName = "Italian" },
            };

            var cuisinesDTO = new List<CuisineDTO>()
            {
                new CuisineDTO { Cuisine = "International" },
                new CuisineDTO { Cuisine = "Ukranian" },
                new CuisineDTO { Cuisine = "Polish" },
                new CuisineDTO { Cuisine = "English" },
                new CuisineDTO { Cuisine = "Italian" },
            };

            var query = new GetAllCuisinesQuery();

            cuisineRepositoryMock
                .Setup(repo => repo.GetAll())
                .ReturnsAsync(cuisines);

            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(mapper => mapper.Map<CuisineDTO>(It.IsAny<Cuisine>()))
                .Returns((Cuisine c) => new CuisineDTO { Cuisine = c.CuisineName });

            var handler = new GetAllCuisinesQueryHandler(unitOfWorkMock.Object, mapperMock.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            var check = true;

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i].Cuisine != cuisinesDTO[i].Cuisine)
                {
                    check = false;
                    break;
                }
            }

            Assert.True(check);

        }

        [Fact]
        public async void GetCuisineId()
        {
            var query = new FindCuisineByIdQuery(1);


            var cuisine = new Cuisine { CuisineName = "Ukranian", Id = 1 };
            var cuisineDto = new CuisineDTO { Cuisine = "Ukrainian" };

            cuisineRepositoryMock
                .Setup(repo => repo.FindById(1))
                .ReturnsAsync(cuisine);

            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(mapper => mapper.Map<CuisineDTO>(cuisine))
                .Returns(cuisineDto);

            var handler = new FindCuisineByIdQueryHandler(unitOfWorkMock.Object, mapperMock.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(cuisineDto.Cuisine, result.Cuisine);

        }
    }
}
