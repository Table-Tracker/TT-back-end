using System.Threading.Tasks;

using TableTracker.Domain.Entities;

namespace TableTracker.Domain.Interfaces.Repositories
{
    public interface IFranchiseRepository : IRepository<Franchise, long>
    {
        Task<Franchise> GetFranchiseByName(string name);
    }
}
