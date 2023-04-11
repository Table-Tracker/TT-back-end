using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using TableTracker.Domain.Entities;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Infrastructure.Repositories
{
    public abstract class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class, IEntity<TId>
    {
        protected readonly TableDbContext _context;

        public Repository(TableDbContext context)
        {
            _context = context;
        }

        public virtual async Task<TEntity> FindById(TId id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<ICollection<TEntity>> GetAll()
        {
            return await _context
                .Set<TEntity>()
                .ToListAsync();
        }

        public virtual async Task Insert(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public virtual async Task<bool> Contains(TEntity entity)
        {
            return await _context
                .Set<TEntity>()
                .ContainsAsync(entity);
        }
    }
}
