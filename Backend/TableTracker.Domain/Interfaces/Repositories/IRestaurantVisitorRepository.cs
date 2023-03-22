using System.Collections.Generic;
using System.Threading.Tasks;

using TableTracker.Domain.Entities;

namespace TableTracker.Domain.Interfaces.Repositories
{
    public interface IRestaurantVisitorRepository : IRepository<RestaurantVisitor, long>
    {
        Task<ICollection<RestaurantVisitor>> GetAllVisitorsByTimesVisited(int numberVisited, Restaurant restaurant);

        Task<ICollection<RestaurantVisitor>> GetAllVisitorsByAverageMoneySpent(double averageMoneySpent, Restaurant restaurant);
    }
}
