using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using TableTracker.Domain.Entities;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Infrastructure.Repositories
{
    public class RestaurantVisitorRepository : Repository<RestaurantVisitor, long>, IRestaurantVisitorRepository
    {
        public RestaurantVisitorRepository(TableDbContext context)
            : base(context)
        {
        }

        public async Task<ICollection<RestaurantVisitor>> GetAllVisitorsByAverageMoneySpent(double averageMoneySpent, Restaurant restaurant)
        {
            return await _context
                .Set<RestaurantVisitor>()
                .Include(x => x.Restaurant)
                .Include(x => x.Visitor)
                .Where(x => x.AverageMoneySpent == averageMoneySpent)
                .Where(x => x.RestaurantId == restaurant.Id)
                .ToListAsync();
        }

        public async Task<ICollection<RestaurantVisitor>> GetAllVisitorsByTimesVisited(int numberVisited, Restaurant restaurant)
        {
            return await _context
                .Set<RestaurantVisitor>()
                .Include(x => x.Restaurant)
                .Include(x => x.Visitor)
                .Where(x => x.TimesVisited == numberVisited)
                .Where(x => x.RestaurantId == restaurant.Id)
                .ToListAsync();
        }
    }
}
