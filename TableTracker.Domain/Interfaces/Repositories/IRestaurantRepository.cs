using System.Collections.Generic;
using System.Threading.Tasks;

using TableTracker.Domain.Entities;
using TableTracker.Domain.Enums;

namespace TableTracker.Domain.Interfaces.Repositories
{
    public interface IRestaurantRepository : IRepository<Restaurant, long>
    {
        Task<ICollection<Restaurant>> GetAllRestaurantsWithFiltering(
            string name = null,
            string address = null,
            float? rating = null,
            IEnumerable<Cuisine> cuisines = null,
            int price = 0,
            RestaurantType? type = null,
            Discount? discount = null,
            Franchise franchise = null);
    }
}
