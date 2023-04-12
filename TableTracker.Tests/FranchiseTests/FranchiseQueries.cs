using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableTracker.Domain.Interfaces.Repositories;
using TableTracker.Domain.Interfaces;
using Xunit;
using System.Formats.Asn1;
using AutoMapper;
using System.Threading;
using TableTracker.Application.CQRS.Cuisines.Queries.FindCuisineById;
using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Application.CQRS.Franchises.Queries.FindFranchiseById;
using TableTracker.Application.CQRS.Franchises.Queries.GetFranchiseByName;
using TableTracker.Application.CQRS.Cuisines.Queries.GetAllCuisines;
using TableTracker.Application.CQRS.Franchises.Queries.GetAllFranchises;

namespace TableTracker.Tests.FranchiseTests
{
    public class FranchiseQueries
    {
        private Mock<IUnitOfWork<long>> unitOfWorkMock;
        private Mock<IFranchiseRepository> _franchiseRepository;

        public FranchiseQueries()
        {
            unitOfWorkMock = new Mock<IUnitOfWork<long>>();
            _franchiseRepository = new Mock<IFranchiseRepository>();

            unitOfWorkMock
                .Setup(uow => uow.GetRepository<IFranchiseRepository>())
                .Returns(_franchiseRepository.Object);
        }

        [Fact]
        public async void FindFranchiseById()
        {
            var query = new FindFranchiseByIdQuery(1);

            var franchise = new Franchise { Name = "McDonalds", Id = 1 };
            var franchiseDto = new FranchiseDTO { Name = "McDonalds" };

            _franchiseRepository
                .Setup(repo => repo.FindById(1))
                .ReturnsAsync(franchise);

            var mapperMock = new Mock<IMapper>();
            mapperMock
                .Setup(mapper => mapper.Map<FranchiseDTO>(franchise))
                .Returns(franchiseDto);

            var handler = new FindFranchiseByIdQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(franchiseDto.Name, result.Name);
        }

        [Fact]
        public async void FindFranchiseByName()
        {
            var query = new GetFranchiseByNameQuery("McDonalds");

            var franchise = new Franchise { Name = "McDonalds" };
            var franchiseDto = new FranchiseDTO { Name = "McDonalds" };

            _franchiseRepository
                .Setup(repo => repo.GetFranchiseByName("McDonalds"))
                .ReturnsAsync(franchise);

            var mapperMock = new Mock<IMapper>();
            mapperMock
                .Setup(mapper => mapper.Map<FranchiseDTO>(franchise))
                .Returns(franchiseDto);

            var handler = new GetFranchiseByNameQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(franchiseDto.Name, result.Name);
        }

        [Fact]
        public async void GetAllFranchises()
        {
            var franchises = new List<Franchise>()
            {
                new Franchise { Name = "McDonalds" },
                new Franchise { Name = "McDonalds1" },
                new Franchise { Name = "McDonalds2" },
                new Franchise { Name = "McDonalds3" },
                new Franchise { Name = "McDonalds4" },
            };

            var cuisinesDTO = new List<FranchiseDTO>()
            {
                new FranchiseDTO { Name = "McDonalds" },
                new FranchiseDTO { Name = "McDonalds1" },
                new FranchiseDTO { Name = "McDonalds2" },
                new FranchiseDTO { Name = "McDonalds3" },
                new FranchiseDTO { Name = "McDonalds4" },
            };

            var query = new GetAllFranchisesQuery();

            _franchiseRepository
                .Setup(repo => repo.GetAll())
                .ReturnsAsync(franchises);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<FranchiseDTO>(It.IsAny<Franchise>()))
                .Returns((Franchise c) => new FranchiseDTO { Name = c.Name });

            var handler = new GetAllFranchisesQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            var check = true;

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i].Name != cuisinesDTO[i].Name)
                {
                    check = false;
                    break;
                }
            }

            Assert.True(check);
        }
    }
}
