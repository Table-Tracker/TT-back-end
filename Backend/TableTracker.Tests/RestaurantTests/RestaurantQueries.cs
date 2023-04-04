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
using TableTracker.Application.CQRS.Managers.Queries.FindManagerById;
using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Infrastructure.Repositories;
using TableTracker.Application.CQRS.Restaurants.Queries.FindRestaurantById;
using TableTracker.Application.CQRS.Franchises.Queries.GetAllFranchises;
using TableTracker.Application.CQRS.Restaurants.Queries.GetAllRestaurants;
using TableTracker.Application.CQRS.Restaurants.Queries.GetAllRestaurantsWithFiltering;

namespace TableTracker.Tests.RestaurantTests
{
    public class RestaurantQueries
    {
        private Mock<IUnitOfWork<long>> unitOfWorkMock;
        private Mock<IRestaurantRepository> _restaurantRepository;

        public RestaurantQueries()
        {
            unitOfWorkMock = new Mock<IUnitOfWork<long>>();
            _restaurantRepository = new Mock<IRestaurantRepository>();

            unitOfWorkMock
                .Setup(uow => uow.GetRepository<IRestaurantRepository>())
                .Returns(_restaurantRepository.Object);
        }

        [Fact]
        public async void FindRestaurantById()
        {
            var query = new FindRestaurantByIdQuery(1);

            var restaurant = new Restaurant { Id = 1 };
            var restaurantDTO = new RestaurantDTO { Id = 1 };

            _restaurantRepository
                .Setup(repo => repo.FindById(1))
                .ReturnsAsync(restaurant);

            var mapperMock = new Mock<IMapper>();
            mapperMock
                .Setup(mapper => mapper.Map<RestaurantDTO>(restaurant))
                .Returns(restaurantDTO);

            var handler = new FindRestaurantByIdQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(restaurant.Id, result.Id);
        }

        [Fact]
        public async void GetAllRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant { Id = 1, Name = "McDonalds" },
                new Restaurant { Id = 1, Name = "McDonalds1"  },
                new Restaurant { Id = 1, Name = "McDonalds2" },
                new Restaurant { Id = 1, Name = "McDonalds3" },
                new Restaurant { Id = 1, Name = "McDonalds4" },
            };

            var restaurantsDTO = new List<RestaurantDTO>()
            {
                new RestaurantDTO { Id = 1, Name = "McDonalds" },
                new RestaurantDTO { Id = 2, Name = "McDonalds1" },
                new RestaurantDTO { Id = 3, Name = "McDonalds2" },
                new RestaurantDTO { Id = 4, Name = "McDonalds3" },
                new RestaurantDTO { Id = 5, Name = "McDonalds4" },
            };

            var query = new GetAllRestaurantsQuery();

            _restaurantRepository
                .Setup(repo => repo.GetAll())
                .ReturnsAsync(restaurants);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<RestaurantDTO>(It.IsAny<Restaurant>()))
                .Returns((Restaurant c) => new RestaurantDTO { Name = c.Name, Id = c.Id });

            var handler = new GetAllRestaurantsQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            var check = true;

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i].Name != restaurantsDTO[i].Name)
                {
                    check = false;
                    break;
                }
            }

            Assert.True(check);
        }

        //[Fact]
        //public async void GetAllRestaurantsWithFilter()
        //{
        //    var restaurants = new List<Restaurant>()
        //    {
        //        new Restaurant { Id = 1, Name = "McDonalds" },
        //        new Restaurant { Id = 1, Name = "McDonalds1"  },
        //        new Restaurant { Id = 1, Name = "McDonalds2" },
        //        new Restaurant { Id = 1, Name = "McDonalds3" },
        //        new Restaurant { Id = 1, Name = "McDonalds4" },
        //    };

        //    var restaurantsDTO = new List<RestaurantDTO>()
        //    {
        //        new RestaurantDTO { Id = 1, Name = "McDonalds" },
        //        new RestaurantDTO { Id = 2, Name = "McDonalds1" },
        //        new RestaurantDTO { Id = 3, Name = "McDonalds2" },
        //        new RestaurantDTO { Id = 4, Name = "McDonalds3" },
        //        new RestaurantDTO { Id = 5, Name = "McDonalds4" },
        //    };

        //    var query = new GetAllRestaurantsWithFilteringQuery();

        //    _restaurantRepository
        //        .Setup(repo => repo.GetAll())
        //        .ReturnsAsync(restaurants);

        //    var mapperMock = new Mock<IMapper>();
        //    mapperMock.Setup(mapper => mapper.Map<RestaurantDTO>(It.IsAny<Restaurant>()))
        //        .Returns((Restaurant c) => new RestaurantDTO { Name = c.Name, Id = c.Id });

        //    var handler = new GetAllRestaurantsQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
        //    var result = await handler.Handle(query, CancellationToken.None);

        //    var check = true;

        //    for (int i = 0; i < result.Length; i++)
        //    {
        //        if (result[i].Name != restaurantsDTO[i].Name)
        //        {
        //            check = false;
        //            break;
        //        }
        //    }

        //    Assert.True(check);
        //}

    }
}
