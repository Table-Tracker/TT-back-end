using System;

using Microsoft.EntityFrameworkCore;

using TableTracker.Domain.Interfaces.Repositories;
using TableTracker.Infrastructure;
using TableTracker.Infrastructure.Repositories;

namespace TableTracker.Tests.UnitOfWork
{
    public class UnitOfWorkFixture : IDisposable
    {
        private readonly TableDbContext _context;

        public UnitOfWorkFixture()
        {
            var opts = new DbContextOptionsBuilder<TableDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new TableDbContext(opts);
            UnitOfWork = new UnitOfWork<Guid>(_context);
            UnitOfWork.RegisterRepositories(typeof(IRepository<,>).Assembly, typeof(Repository<,>).Assembly);
        }

        public UnitOfWork<Guid> UnitOfWork { get; }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
