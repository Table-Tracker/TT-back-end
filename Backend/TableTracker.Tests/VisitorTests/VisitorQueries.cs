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
using TableTracker.Application.CQRS.Users.Queries.FindUserById;
using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Infrastructure.Repositories;
using Xunit;
using TableTracker.Application.CQRS.Visitors.Queries.FindVisitorById;
using TableTracker.Application.CQRS.VisitorHistories.Queries.GetAllVisits;
using TableTracker.Application.CQRS.Visitors.Queries.GetAllVisitors;
using TableTracker.Application.CQRS.Visitors.Queries.GetAllVisitorsByTrustFactor;

namespace TableTracker.Tests.VisitorTests
{
    public class VisitorQueries
    {
        private Mock<IUnitOfWork<long>> unitOfWorkMock;
        private Mock<IVisitorRepository> _visitorRepository;

        public VisitorQueries()
        {
            unitOfWorkMock = new Mock<IUnitOfWork<long>>();
            _visitorRepository = new Mock<IVisitorRepository>();

            unitOfWorkMock
                .Setup(uow => uow.GetRepository<IVisitorRepository>())
                .Returns(_visitorRepository.Object);
        }

        [Fact]
        public async void FindVisitorById()
        {
            var query = new FindVisitorByIdQuery(1);

            var user = new Visitor { Id = 1 };
            var userDTO = new VisitorDTO { Id = 1 };

            _visitorRepository
                .Setup(repo => repo.FindById(1))
                .ReturnsAsync(user);

            var mapperMock = new Mock<IMapper>();
            mapperMock
                .Setup(mapper => mapper.Map<VisitorDTO>(user))
                .Returns((Visitor c) => new VisitorDTO { Id = c.Id});

            var handler = new FindVisitorByIdQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(user.Id, result.Id);
        }

        [Fact]
        public async void GetAllVisitors()
        {
            var visitors = new List<Visitor>()
            {
                new Visitor { Id = 1 },
                new Visitor { Id = 2 },
                new Visitor { Id = 3 },
                new Visitor { Id = 4 },
                new Visitor { Id = 5 },
            };

            var visitorHistoryDTO = new List<VisitorDTO>()
            {
                new VisitorDTO { Id = 1 },
                new VisitorDTO { Id = 2 },
                new VisitorDTO { Id = 3 },
                new VisitorDTO { Id = 4 },
                new VisitorDTO { Id = 5 },
            };

            var query = new GetAllVisitorsQuery();

            _visitorRepository
                .Setup(repo => repo.GetAll())
                .ReturnsAsync(visitors);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<VisitorDTO>(It.IsAny<Visitor>()))
                .Returns((Visitor c) => new VisitorDTO { Id = c.Id });

            var handler = new GetAllVisitorsQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            var check = true;

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i].Id != visitorHistoryDTO[i].Id)
                {
                    check = false;
                    break;
                }
            }

            Assert.True(check);
        }

        [Fact]
        public async void GetAllVisitorsByTrustFactor()
        {
            var visitors = new List<Visitor>()
            {
                new Visitor { Id = 1, GeneralTrustFactor = 1 },
                new Visitor { Id = 2, GeneralTrustFactor = 1 },
                new Visitor { Id = 3 },
                new Visitor { Id = 4 },
                new Visitor { Id = 5 },
            };

            var visitorHistoryDTO = new List<VisitorDTO>()
            {
                new VisitorDTO { Id = 1, GeneralTrustFactor = 1 },
                new VisitorDTO { Id = 2, GeneralTrustFactor = 1 },
            };

            var query = new GetAllVisitorsByTrustFactorQuery(1);

            _visitorRepository
                .Setup(repo => repo.GetAllVisitorsByTrustFactor(1))
                .ReturnsAsync(new List<Visitor>(visitors.Take(2)));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<VisitorDTO>(It.IsAny<Visitor>()))
                .Returns((Visitor c) => new VisitorDTO { Id = c.Id });

            var handler = new GetAllVisitorsByTrustFactorQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            var check = true;

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i].Id != visitorHistoryDTO[i].Id)
                {
                    check = false;
                    break;
                }
            }

            Assert.True(check);
        }
    }
}
