using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using TableTracker.Domain.Entities;
using TableTracker.Domain.Enums;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Infrastructure.Repositories
{
    public class RestaurantRepository : Repository<Restaurant, long>, IRestaurantRepository
    {
        public RestaurantRepository(TableDbContext context)
            : base(context)
        {
        }

        public override async Task<Restaurant> FindById(long id)
        {
            return await _context
                .Restaurants
                .Include(x => x.Tables)
                .Include(x => x.Layout)
                .Include(x => x.Franchise)
                .Include(x => x.MainImage)
                .Include(x => x.Cuisines)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<ICollection<Restaurant>> GetAll()
        {
            return await _context
                .Restaurants
                .Include(x => x.Tables)
                .Include(x => x.Layout)
                .Include(x => x.Franchise)
                .Include(x => x.MainImage)
                .Include(x => x.Cuisines)
                .ToListAsync();
        }

        public async Task<ICollection<Restaurant>> GetAllRestaurantsWithFiltering(
            string name = null,
            string address = null,
            float? rating = null,
            IEnumerable<Cuisine> cuisines = null,
            int price = 0,
            RestaurantType? type = null,
            Discount? discount = null,
            Franchise franchise = null)
        {
            return await _context
                .Set<Restaurant>()
                .Include(x => x.Tables)
                .Include(x => x.Layout)
                .Include(x => x.Franchise)
                .Include(x => x.MainImage)
                .Include(x => x.Cuisines)
                .Where(x => name == null || x.Name == name)
                .Where(x => address == null || x.Address == address)
                .Where(x => cuisines == null || x.Cuisines.All(x => cuisines.Any(y => y.CuisineName == x.CuisineName)))
                .Where(x => price == 0 || x.PriceRange == price)
                .Where(x => !type.HasValue || x.Type == type)
                .Where(x => !discount.HasValue || x.Discount == discount)
                .Where(x => franchise == null || x.Franchise == franchise)
                .ToListAsync();
        }
    }
}
