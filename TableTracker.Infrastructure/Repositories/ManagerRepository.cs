using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using TableTracker.Domain.Entities;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Infrastructure.Repositories
{
    public class ManagerRepository : Repository<Manager, long>, IManagerRepository
    {
        public ManagerRepository(TableDbContext context)
            : base(context)
        {
        }

        public async Task<ICollection<Manager>> FilterManagers(string filter)
        {
            return await _context
                .Set<Manager>()
                .Include(x => x.Restaurant)
                .Where(x => x.FullName.Contains(filter) || x.Email.Contains(filter))
                .ToListAsync();
        }

        public async Task<Manager> FindManagerByRestaurant(long restaurant)
        {
            return await _context
                .Set<Manager>()
                .Include(x => x.Restaurant)
                .FirstOrDefaultAsync(x => x.RestaurantId == restaurant);
        }
    }
}
