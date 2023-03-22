using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using TableTracker.Domain.Entities;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Infrastructure.Repositories
{
    public class FranchiseRepository : Repository<Franchise, long>, IFranchiseRepository
    {
        public FranchiseRepository(TableDbContext context)
            : base(context)
        {
        }

        public async Task<Franchise> GetFranchiseByName(string name)
        {
            return await _context
                .Set<Franchise>()
                .Include(x => x.Restaurants)
                .FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
