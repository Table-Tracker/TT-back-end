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
using TableTracker.Application.CQRS.Franchises.Queries.FindFranchiseById;
using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Infrastructure.Repositories;
using TableTracker.Application.CQRS.Layout.Queries.FindLayoutById;
using TableTracker.Application.CQRS.Layout.Queries.FindLayoutByRestaurant;
using Xunit.Sdk;

namespace TableTracker.Tests.LayoutTests
{
    public class LayoutQueries
    {
        private Mock<IUnitOfWork<long>> unitOfWorkMock;
        private Mock<ILayoutRepository> _layoutRepository;

        public LayoutQueries()
        {
            unitOfWorkMock = new Mock<IUnitOfWork<long>>();
            _layoutRepository = new Mock<ILayoutRepository>();

            unitOfWorkMock
                .Setup(uow => uow.GetRepository<ILayoutRepository>())
                .Returns(_layoutRepository.Object);
        }

        [Fact]
        public async void FindLayoutById()
        {
            var query = new FindLayoutByIdQuery(1);

            var layout = new Layout { LayoutData = 1, Id = 1 };
            var layoutDTO = new LayoutDTO { LayoutData = 1 };

            _layoutRepository
                .Setup(repo => repo.FindById(1))
                .ReturnsAsync(layout);

            var mapperMock = new Mock<IMapper>();
            mapperMock
                .Setup(mapper => mapper.Map<LayoutDTO>(layout))
                .Returns(layoutDTO);

            var handler = new FindLayoutByIdQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(layoutDTO.LayoutData, result.LayoutData);
        }

        [Fact]
        public async void FindLayoutByRestaurant()
        {
            var query = new FindLayoutByRestaurantQuery(1);

            var restaurant = new Restaurant { Id = 1, Email = "LMAO@g.com" };
            var resDto = new RestaurantDTO { Id = 1, Email = "LMAO@g.com" };
            var layout = new Layout { LayoutData = 1, Id = 1, RestaurantId = 1, Restaurant = restaurant };
            var layoutDTO = new LayoutDTO { LayoutData = 1, Restaurant = resDto, Id = 1 };

            _layoutRepository
                .Setup(repo => repo.FindLayoutByRestaurant(1))
                .ReturnsAsync(layout);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<LayoutDTO>(It.IsAny<Layout>()))
                 .Returns((Layout c) => new LayoutDTO { LayoutData = c.LayoutData, Id = c.Id, 
                     Restaurant = new RestaurantDTO 
                     {
                         Id = c.Restaurant.Id,
                         Email = c.Restaurant.Email
                     } });

            var handler = new FindLayoutByRestaurantQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(layoutDTO.LayoutData, result.LayoutData);
            Assert.Equal(result.Restaurant.Email, layoutDTO.Restaurant.Email);
        }
    }
}
