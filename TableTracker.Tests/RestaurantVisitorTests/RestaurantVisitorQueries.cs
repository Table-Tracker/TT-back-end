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
using TableTracker.Application.CQRS.Franchises.Queries.GetAllFranchises;
using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Infrastructure.Repositories;
using TableTracker.Application.CQRS.RestaurantVisitors.Queries.GetAllRestaurantVisitors;
using TableTracker.Application.CQRS.RestaurantVisitors.Queries.GetAllVisitorsByAverageMoneySpent;
using TableTracker.Application.CQRS.RestaurantVisitors.Queries.GetAllVisitorsByTimesVisited;
using TableTracker.Application.CQRS.Franchises.Queries.FindFranchiseById;
using TableTracker.Application.CQRS.RestaurantVisitors.Queries.GetRestaurantVisitorById;

namespace TableTracker.Tests.RestaurantVisitorTests
{
    public class RestaurantVisitorQueries
    {
        private Mock<IUnitOfWork<long>> unitOfWorkMock;
        private Mock<IRestaurantVisitorRepository> _restaurantVisitorRepository;

        public RestaurantVisitorQueries()
        {
            unitOfWorkMock = new Mock<IUnitOfWork<long>>();
            _restaurantVisitorRepository = new Mock<IRestaurantVisitorRepository>();

            unitOfWorkMock
                .Setup(uow => uow.GetRepository<IRestaurantVisitorRepository>())
                .Returns(_restaurantVisitorRepository.Object);
        }

        [Fact]
        public async void GetAllRestaurantVisitors()
        {
            var restaurantVisitor = new List<RestaurantVisitor>()
            {
                new RestaurantVisitor { Id = 1, },
                new RestaurantVisitor { Id = 2 },
                new RestaurantVisitor { Id = 3 },
                new RestaurantVisitor { Id = 4 },
                new RestaurantVisitor { Id = 5 },
            };

            var restaurantVisitorDTO = new List<RestaurantVisitorDTO>()
            {
                new RestaurantVisitorDTO { Id = 1 },
                new RestaurantVisitorDTO { Id = 2 },
                new RestaurantVisitorDTO { Id = 3 },
                new RestaurantVisitorDTO { Id = 4 },
                new RestaurantVisitorDTO { Id = 5 },
            };

            var query = new GetAllRestaurantVisitorsQuery();

            _restaurantVisitorRepository
                .Setup(repo => repo.GetAll())
                .ReturnsAsync(restaurantVisitor);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<RestaurantVisitorDTO>(It.IsAny<RestaurantVisitor>()))
                .Returns((RestaurantVisitor c) => new RestaurantVisitorDTO { Id = c.Id });

            var handler = new GetAllRestaurantVisitorsQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            var check = true;

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i].Id != restaurantVisitorDTO[i].Id)
                {
                    check = false;
                    break;
                }
            }

            Assert.True(check);
        }

        [Fact]
        public async void GetAllVisitorsByAverageMoneySpent()
        {
            var restaurant = new Restaurant { Id = 1, Email = "LMAO@g.com" };
            var resDto = new RestaurantDTO { Id = 1, Email = "LMAO@g.com" };

            var restaurantVisitor = new List<RestaurantVisitor>()
            {
                new RestaurantVisitor { Id = 1, AverageMoneySpent = 1337, RestaurantId = 1, Restaurant = restaurant },
                new RestaurantVisitor { Id = 2, AverageMoneySpent = 1337, RestaurantId = 1, Restaurant = restaurant },
                new RestaurantVisitor { Id = 3, AverageMoneySpent = 0, RestaurantId = 1, Restaurant = restaurant },
                new RestaurantVisitor { Id = 4, AverageMoneySpent = 1, RestaurantId = 1, Restaurant = restaurant },
                new RestaurantVisitor { Id = 5, AverageMoneySpent = 1, RestaurantId = 1, Restaurant = restaurant },
            };

            var restaurantVisitorDTO = new List<RestaurantVisitorDTO>()
            {
                new RestaurantVisitorDTO { Id = 1, AverageMoneySpent = 1337, Restaurant = resDto },
                new RestaurantVisitorDTO { Id = 2, AverageMoneySpent = 1337, Restaurant = resDto },
            };

            var query = new GetAllVisitorsByAverageMoneySpentQuery(1337, resDto);

            _restaurantVisitorRepository
                .Setup(repo => repo.GetAllVisitorsByAverageMoneySpent(1337, restaurant))
                .ReturnsAsync(new List<RestaurantVisitor>(restaurantVisitor.Take(2)));

            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(mapper => mapper.Map<Restaurant>(resDto))
                .Returns(restaurant);
            mapperMock.Setup(mapper => mapper.Map<RestaurantVisitorDTO>(It.IsAny<RestaurantVisitor>()))
                .Returns((RestaurantVisitor c) => new RestaurantVisitorDTO { Id = c.Id });

            var handler = new GetAllVisitorsByAverageMoneySpentQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            var check = true;

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i].Id != restaurantVisitorDTO[i].Id)
                {
                    check = false;
                    break;
                }
            }

            Assert.True(check);
        }

        [Fact]
        public async void GetAllVisitorsByTimesVisited()
        {
            var restaurant = new Restaurant { Id = 1, Email = "LMAO@g.com" };
            var resDto = new RestaurantDTO { Id = 1, Email = "LMAO@g.com" };

            var restaurantVisitor = new List<RestaurantVisitor>()
            {
                new RestaurantVisitor { Id = 1, TimesVisited = 1337, RestaurantId = 1, Restaurant = restaurant },
                new RestaurantVisitor { Id = 2, TimesVisited = 1337, RestaurantId = 1, Restaurant = restaurant },
                new RestaurantVisitor { Id = 3, TimesVisited = 0, RestaurantId = 1, Restaurant = restaurant },
                new RestaurantVisitor { Id = 4, TimesVisited = 1, RestaurantId = 1, Restaurant = restaurant },
                new RestaurantVisitor { Id = 5, TimesVisited = 1, RestaurantId = 1, Restaurant = restaurant },
            };

            var restaurantVisitorDTO = new List<RestaurantVisitorDTO>()
            {
                new RestaurantVisitorDTO { Id = 1, TimesVisited = 1337, Restaurant = resDto },
                new RestaurantVisitorDTO { Id = 2, TimesVisited = 1337, Restaurant = resDto },
            };

            var query = new GetAllVisitorsByTimesVisitedQuery(1337, resDto);

            _restaurantVisitorRepository
                .Setup(repo => repo.GetAllVisitorsByTimesVisited(1337, restaurant))
                .ReturnsAsync(new List<RestaurantVisitor>(restaurantVisitor.Take(2)));

            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(mapper => mapper.Map<Restaurant>(resDto))
                .Returns(restaurant);
            mapperMock.Setup(mapper => mapper.Map<RestaurantVisitorDTO>(It.IsAny<RestaurantVisitor>()))
                .Returns((RestaurantVisitor c) => new RestaurantVisitorDTO { Id = c.Id });

            var handler = new GetAllVisitorsByTimesVisitedQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            var check = true;

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i].Id != restaurantVisitorDTO[i].Id)
                {
                    check = false;
                    break;
                }
            }

            Assert.True(check);
        }

        [Fact]
        public async void GetRestaurantVisitorById()
        {
            var query = new GetRestaurantVisitorByIdQuery(1);

            var restaurantVisitor = new RestaurantVisitor { Id = 1 };
            var restaurantVisitorDTO = new RestaurantVisitorDTO { Id = 1 };

            _restaurantVisitorRepository
                .Setup(repo => repo.FindById(1))
                .ReturnsAsync(restaurantVisitor);

            var mapperMock = new Mock<IMapper>();
            mapperMock
                .Setup(mapper => mapper.Map<RestaurantVisitorDTO>(restaurantVisitor))
                .Returns(restaurantVisitorDTO);

            var handler = new GetRestaurantVisitorByIdQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(restaurantVisitorDTO.Id, result.Id);
        }
    }
}
