using System.Threading.Tasks;

using TableTracker.Domain.Entities;

namespace TableTracker.Domain.Interfaces.Repositories
{
    public interface ILayoutRepository : IRepository<Layout, long>
    {
        Task<Layout> FindLayoutByRestaurant(long restaurant);
    }
}
