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
using TableTracker.Application.CQRS.Restaurants.Queries.FindRestaurantById;
using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Infrastructure.Repositories;
using TableTracker.Application.CQRS.Tables.Queries.FindTableById;
using TableTracker.Application.CQRS.Franchises.Queries.GetAllFranchises;
using TableTracker.Application.CQRS.Tables.Queries.GetAllTables;
using TableTracker.Application.CQRS.Reservations.Queries.GetAllReservations;
using TableTracker.Application.CQRS.Tables.Queries.GetAllTablesByRestaurant;

namespace TableTracker.Tests.TableTests
{
    public class TableQueries
    {
        private Mock<IUnitOfWork<long>> unitOfWorkMock;
        private Mock<ITableRepository> _tableRepository;

        public TableQueries()
        {
            unitOfWorkMock = new Mock<IUnitOfWork<long>>();
            _tableRepository = new Mock<ITableRepository>();

            unitOfWorkMock
                .Setup(uow => uow.GetRepository<ITableRepository>())
                .Returns(_tableRepository.Object);
        }

        [Fact]
        public async void FindTableById()
        {
            var query = new FindTableByIdQuery(1);

            var table = new Table { Id = 1 };
            var tableDTO = new TableDTO { Id = 1 };

            _tableRepository
                .Setup(repo => repo.FindById(1))
                .ReturnsAsync(table);

            var mapperMock = new Mock<IMapper>();
            mapperMock
                .Setup(mapper => mapper.Map<TableDTO>(table))
                .Returns(tableDTO);

            var handler = new FindTableByIdQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(table.Id, result.Id);
        }

        [Fact]
        public async void GetAlltables()
        {
            var tables = new List<Table>()
            {
                new Table { Id = 1 },
                new Table { Id = 2 },
                new Table { Id = 3 },
                new Table { Id = 4 },
                new Table { Id = 5 },
            };

            var tablesDTO = new List<TableDTO>()
            {
                new TableDTO { Id = 1 },
                new TableDTO { Id = 2 },
                new TableDTO { Id = 3 },
                new TableDTO { Id = 4 },
                new TableDTO { Id = 5 },
            };

            var query = new GetAllTablesQuery();

            _tableRepository
                .Setup(repo => repo.GetAll())
                .ReturnsAsync(tables);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<TableDTO>(It.IsAny<Table>()))
                .Returns((Table c) => new TableDTO { Id = c.Id });

            var handler = new GetAllTablesQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            var check = true;

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i].Id != tablesDTO[i].Id)
                {
                    check = false;
                    break;
                }
            }

            Assert.True(check);
        }

        [Fact]
        public async void GetAllTablesByRestaurant()
        {
            var restaurant = new Restaurant { Id = 1, Email = "LMAO@g.com" };
            var resDto = new RestaurantDTO { Id = 1, Email = "LMAO@g.com" };

            var tables = new List<Table>()
            {
                new Table { RestaurantId = 1, Restaurant = restaurant, Id = 1 },
                new Table { RestaurantId = 1, Restaurant = restaurant, Id = 2 },
                new Table { Id = 3 },
                new Table { Id = 4 },
                new Table { Id = 5 },
            };
            var tablesDTO = new List<TableDTO>()
            {
                new TableDTO { Restaurant = resDto, Id = 1 },
                new TableDTO { Restaurant = resDto, Id = 2 },
            };

            var query = new GetAllTablesByRestaurantQuery(1);

            _tableRepository
                .Setup(repo => repo.GetAllTablesByRestaurant(1))
                .ReturnsAsync(new List<Table>(tables.Take(2)));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<TableDTO>(It.IsAny<Table>()))
                 .Returns((Table c) => new TableDTO
                 {
                     Id = c.Id,
                 });

            var handler = new GetAllTablesByRestaurantQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            var check = true;

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i].Id != tablesDTO[i].Id)
                {
                    check = false;
                    break;
                }
            }

            Assert.True(check);
        }
    }
}
