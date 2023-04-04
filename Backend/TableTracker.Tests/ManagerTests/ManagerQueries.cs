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
using TableTracker.Application.CQRS.Layout.Queries.FindLayoutById;
using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Infrastructure.Repositories;
using TableTracker.Application.CQRS.Managers.Queries.FindManagerById;
using TableTracker.Application.CQRS.Managers.Queries.FindManagerByRestaurant;
using TableTracker.Application.CQRS.Franchises.Queries.GetAllFranchises;
using TableTracker.Application.CQRS.Managers.Queries.GetAllManagers;

namespace TableTracker.Tests.ManagerTests
{
    public class ManagerQueries
    {
        private Mock<IUnitOfWork<long>> unitOfWorkMock;
        private Mock<IManagerRepository> _managerRepository;

        public ManagerQueries()
        {
            unitOfWorkMock = new Mock<IUnitOfWork<long>>();
            _managerRepository = new Mock<IManagerRepository>();

            unitOfWorkMock
                .Setup(uow => uow.GetRepository<IManagerRepository>())
                .Returns(_managerRepository.Object);
        }

        [Fact]
        public async void FindManagerById()
        {
            var query = new FindManagerByIdQuery(1);

            var manager = new Manager { Id = 1, FullName = "John Smith" };
            var managerDTO = new ManagerDTO { FullName = "John Smith" };

            _managerRepository
                .Setup(repo => repo.FindById(1))
                .ReturnsAsync(manager);

            var mapperMock = new Mock<IMapper>();
            mapperMock
                .Setup(mapper => mapper.Map<ManagerDTO>(manager))
                .Returns(managerDTO);

            var handler = new FindManagerByIdQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(manager.FullName, result.FullName);
        }

        [Fact]
        public async void FindManagerByRestaurant()
        {
            var query = new FindManagerByRestaurantQuery(1);

            var restaurant = new Restaurant { Id = 1, Email = "LMAO@g.com" };
            var resDto = new RestaurantDTO { Id = 1, Email = "LMAO@g.com" };
            var manager = new Manager { Id = 1, FullName = "John Smith", Restaurant = restaurant };
            var managerDTO = new ManagerDTO { FullName = "John Smith", Restaurant = resDto };

            _managerRepository
                .Setup(repo => repo.FindManagerByRestaurant(1))
                .ReturnsAsync(manager);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<ManagerDTO>(It.IsAny<Manager>()))
                 .Returns((Manager c) => new ManagerDTO
                 {
                     FullName = c.FullName,
                     Id = c.Id,
                     Restaurant = new RestaurantDTO
                     {
                         Id = c.Restaurant.Id,
                         Email = c.Restaurant.Email
                     }
                 });

            var handler = new FindManagerByRestaurantQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(manager.FullName, result.FullName);
            Assert.Equal(manager.Restaurant.Email, result.Restaurant.Email);
        }

        [Fact]
        public async void GetAllManagers()
        {
            var managers = new List<Manager>()
            {
                new Manager { FullName = "John Smith" },
                new Manager { FullName = "John Smith1" },
                new Manager { FullName = "John Smith2" },
                new Manager { FullName = "John Smith3" },
                new Manager { FullName = "John Smith4" },
            };

            var managerDTO = new List<ManagerDTO>()
            {
                new ManagerDTO { FullName = "John Smith" },
                new ManagerDTO { FullName = "John Smith1" },
                new ManagerDTO { FullName = "John Smith2" },
                new ManagerDTO { FullName = "John Smith3" },
                new ManagerDTO { FullName = "John Smith4" },
            };

            var query = new GetAllManagersQuery();

            _managerRepository
                .Setup(repo => repo.GetAll())
                .ReturnsAsync(managers);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<ManagerDTO>(It.IsAny<Manager>()))
                 .Returns((Manager c) => new ManagerDTO
                 {
                     FullName = c.FullName
                 });

            var handler = new GetAllManagersQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            var check = true;

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i].FullName != managerDTO[i].FullName)
                {
                    check = false;
                    break;
                }
            }

            Assert.True(check);
        }
    }
}
