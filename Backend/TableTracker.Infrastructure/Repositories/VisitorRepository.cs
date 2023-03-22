using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using TableTracker.Domain.Entities;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Infrastructure.Repositories
{
    public class VisitorRepository : Repository<Visitor, long>, IVisitorRepository
    {
        public VisitorRepository(TableDbContext context)
            : base(context)
        {
        }

        public override async Task<Visitor> FindById(long id)
        {
            return await _context
                .Set<Visitor>()
                .Include(x => x.Reservations)
                    .ThenInclude(x => x.Table)
                        .ThenInclude(x => x.Restaurant)
                            .ThenInclude(x => x.MainImage)
                .Include(x => x.Reservations)
                    .ThenInclude(x => x.Table)
                        .ThenInclude(x => x.Restaurant)
                            .ThenInclude(x => x.Cuisines)
                .Include(x => x.Favourites)
                    .ThenInclude(x => x.MainImage)
                .Include(x => x.Avatar)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ICollection<Visitor>> FilterVisitors(string filter)
        {
            return await _context
                .Set<Visitor>()
                .Include(x => x.Reservations)
                    .ThenInclude(x => x.Table)
                        .ThenInclude(x => x.Restaurant)
                            .ThenInclude(x => x.MainImage)
                .Include(x => x.Reservations)
                    .ThenInclude(x => x.Table)
                        .ThenInclude(x => x.Restaurant)
                            .ThenInclude(x => x.Cuisines)
                .Include(x => x.Favourites)
                    .ThenInclude(x => x.MainImage)
                .Include(x => x.Avatar)
                .Where(x => x.FullName.Contains(filter) || x.Email.Contains(filter))
                .ToListAsync();
        }

        public async Task<ICollection<Visitor>> GetAllVisitorsByTrustFactor(float trustFactor)
        {
            return await _context
                .Set<Visitor>()
                .Include(x => x.Reservations)
                    .ThenInclude(x => x.Table)
                        .ThenInclude(x => x.Restaurant)
                            .ThenInclude(x => x.MainImage)
                .Include(x => x.Reservations)
                    .ThenInclude(x => x.Table)
                        .ThenInclude(x => x.Restaurant)
                            .ThenInclude(x => x.Cuisines)
                .Include(x => x.Favourites)
                    .ThenInclude(x => x.MainImage)
                .Include(x => x.Avatar)
                .Where(x => x.GeneralTrustFactor == trustFactor)
                .ToListAsync();
        }

        public async Task<ICollection<Restaurant>> FindVisitorFavouritesByVisitorId(long visitorId)
        {
            var visitor = await _context
                .Set<Visitor>()
                .Include(x => x.Favourites)
                    .ThenInclude(x => x.MainImage)
                .FirstOrDefaultAsync(x => x.Id == visitorId);

            return visitor.Favourites;
        }
    }
}
