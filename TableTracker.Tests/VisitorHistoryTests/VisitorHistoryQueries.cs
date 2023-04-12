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
using TableTracker.Application.CQRS.VisitorHistories.Queries.FindVisitById;
using TableTracker.Application.CQRS.VisitorHistories.Queries.GetAllVisitsByDate;
using TableTracker.Application.CQRS.Restaurants.Queries.GetAllRestaurants;
using TableTracker.Application.CQRS.VisitorHistories.Queries.GetAllVisits;

namespace TableTracker.Tests.VisitorHistoryTests
{
    public class VisitorHistoryQueries
    {
        private Mock<IUnitOfWork<long>> unitOfWorkMock;
        private Mock<IVisitorHistoryRepository> _visitorHistoryRepository;

        public VisitorHistoryQueries()
        {
            unitOfWorkMock = new Mock<IUnitOfWork<long>>();
            _visitorHistoryRepository = new Mock<IVisitorHistoryRepository>();

            unitOfWorkMock
                .Setup(uow => uow.GetRepository<IVisitorHistoryRepository>())
                .Returns(_visitorHistoryRepository.Object);
        }

        [Fact]
        public async void FindVisitorHistoryById()
        {
            var query = new FindVisitByIdQuery(1);

            var visitorHistory = new VisitorHistory { Id = 1 };
            var visitorHistoryDTO = new VisitorHistoryDTO { Id = 1 };

            _visitorHistoryRepository
                .Setup(repo => repo.FindById(1))
                .ReturnsAsync(visitorHistory);

            var mapperMock = new Mock<IMapper>();
            mapperMock
                .Setup(mapper => mapper.Map<VisitorHistoryDTO>(visitorHistory))
                .Returns((VisitorHistory c) => new VisitorHistoryDTO { Id = c.Id});

            var handler = new FindVisitByIdQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(visitorHistoryDTO.Id, result.Id);
        }

        [Fact]
        public async void GetAllVisits()
        {
            var visitorHistories = new List<VisitorHistory>()
            {
                new VisitorHistory { Id = 1 },
                new VisitorHistory { Id = 2 },
                new VisitorHistory { Id = 3 },
                new VisitorHistory { Id = 4 },
                new VisitorHistory { Id = 5 },
            };

            var visitorHistoriesDTO = new List<VisitorHistoryDTO>()
            {
                new VisitorHistoryDTO { Id = 1 },
                new VisitorHistoryDTO { Id = 2 },
                new VisitorHistoryDTO { Id = 3 },
                new VisitorHistoryDTO { Id = 4 },
                new VisitorHistoryDTO { Id = 5 },
            };

            var query = new GetAllVisitsQuery();

            _visitorHistoryRepository
                .Setup(repo => repo.GetAll())
                .ReturnsAsync(visitorHistories);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<VisitorHistoryDTO>(It.IsAny<VisitorHistory>()))
                .Returns((VisitorHistory c) => new VisitorHistoryDTO {  Id = c.Id });

            var handler = new GetAllVisitsQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            var check = true;

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i].Id != visitorHistoriesDTO[i].Id)
                {
                    check = false;
                    break;
                }
            }

            Assert.True(check);
        }

        [Fact]
        public async void GetAllVisitsByDate()
        {
            var visitorHistories = new List<VisitorHistory>()
            {
                new VisitorHistory { Id = 1, DateTime = DateTime.Today },
                new VisitorHistory { Id = 2, DateTime = DateTime.Today },
                new VisitorHistory { Id = 3, },
                new VisitorHistory { Id = 4, },
                new VisitorHistory { Id = 5, },
            };

            var visitorHistoriesDTO = new List<VisitorHistoryDTO>()
            {
                new VisitorHistoryDTO { Id = 1, DateTime = DateTime.Today },
                new VisitorHistoryDTO { Id = 2, DateTime = DateTime.Today },
            };

            var query = new GetAllVisitsByDateQuery(DateTime.Today);

            _visitorHistoryRepository
                .Setup(repo => repo.GetAllVisitsByDate(DateTime.Today))
                .ReturnsAsync(new List<VisitorHistory>(visitorHistories.Take(2)));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<VisitorHistoryDTO>(It.IsAny<VisitorHistory>()))
                .Returns((VisitorHistory c) => new VisitorHistoryDTO { Id = c.Id });

            var handler = new GetAllVisitsByDateQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            var check = true;

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i].Id != visitorHistoriesDTO[i].Id)
                {
                    check = false;
                    break;
                }
            }

            Assert.True(check);
        }

    }
}
