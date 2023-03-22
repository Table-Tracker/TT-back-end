using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using TableTracker.Domain.Entities;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Infrastructure.Repositories
{
    public class LayoutRepository : Repository<Layout, long>, ILayoutRepository
    {
        public LayoutRepository(TableDbContext context)
            : base(context)
        {
        }

        public async Task<Layout> FindLayoutByRestaurant(long restaurant)
        {
            return await _context
                .Set<Layout>()
                .Include(x => x.Restaurant)
                .FirstOrDefaultAsync(x => x.RestaurantId == restaurant);
        }

        public override async Task<Layout> FindById(long id)
        {
            return await _context
                .Set<Layout>()
                .Include(x => x.Restaurant)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
