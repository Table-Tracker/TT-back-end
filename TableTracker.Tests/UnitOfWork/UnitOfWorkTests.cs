using System;

using TableTracker.Domain.Interfaces.Repositories;
using TableTracker.Infrastructure.Repositories;

using Xunit;

namespace TableTracker.Tests.UnitOfWork
{
    public class UnitOfWorkTests : IClassFixture<UnitOfWorkFixture>
    {
        private readonly UnitOfWorkFixture _fixture;

        public UnitOfWorkTests(UnitOfWorkFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void GenerateRepositories_Generates()
        {
            var repo1 = _fixture.UnitOfWork.GetRepository<IFranchiseRepository>();
            var repo2 = _fixture.UnitOfWork.GetRepository<ILayoutRepository>();
            var repo3 = _fixture.UnitOfWork.GetRepository<IManagerRepository>();
            var repo4 = _fixture.UnitOfWork.GetRepository<IReservationRepository>();
            var repo5 = _fixture.UnitOfWork.GetRepository<IRestaurantRepository>();
            var repo6 = _fixture.UnitOfWork.GetRepository<IRestaurantVisitorRepository>();
            var repo7 = _fixture.UnitOfWork.GetRepository<ITableRepository>();
            var repo8 = _fixture.UnitOfWork.GetRepository<IUserRepository>();
            var repo9 = _fixture.UnitOfWork.GetRepository<IVisitorHistoryRepository>();
            var repo10 = _fixture.UnitOfWork.GetRepository<IVisitorRepository>();

            Assert.IsType<FranchiseRepository>(repo1);
            Assert.IsType<LayoutRepository>(repo2);
            Assert.IsType<ManagerRepository>(repo3);
            Assert.IsType<ReservationRepository>(repo4);
            Assert.IsType<RestaurantRepository>(repo5);
            Assert.IsType<RestaurantVisitorRepository>(repo6);
            Assert.IsType<TableRepository>(repo7);
            Assert.IsType<UserRepository>(repo8);
            Assert.IsType<VisitorHistoryRepository>(repo9);
            Assert.IsType<VisitorRepository>(repo10);
        }

        [Fact]
        public void GetRepository_IncorrectParameter_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(_fixture.UnitOfWork.GetRepository<string>);
            Assert.Throws<NotSupportedException>(_fixture.UnitOfWork.GetRepository<FranchiseRepository>);
        }
    }
}