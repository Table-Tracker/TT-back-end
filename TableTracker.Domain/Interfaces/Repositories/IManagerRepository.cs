using System.Collections.Generic;
using System.Threading.Tasks;

using TableTracker.Domain.Entities;

namespace TableTracker.Domain.Interfaces.Repositories
{
    public interface IManagerRepository : IRepository<Manager, long>
    {
        Task<Manager> FindManagerByRestaurant(long restaurant);

        Task<ICollection<Manager>> FilterManagers(string filter);
    }
}
