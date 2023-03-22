using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using TableTracker.Domain.Entities;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Infrastructure.Repositories
{
    public class VisitorHistoryRepository : Repository<VisitorHistory, long>, IVisitorHistoryRepository
    {
        public VisitorHistoryRepository(TableDbContext context)
            : base(context)
        {
        }

        public async Task<ICollection<VisitorHistory>> GetAllVisitsByDate(DateTime date)
        {
            return await _context
                .Set<VisitorHistory>()
                .Include(x => x.Restaurant)
                .Include(x => x.Visitor)
                .Where(x => x.DateTime == date)
                .ToListAsync();
        }
    }
}
