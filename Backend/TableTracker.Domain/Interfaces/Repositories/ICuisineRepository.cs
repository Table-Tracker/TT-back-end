using System.Threading.Tasks;

using TableTracker.Domain.Entities;
using TableTracker.Domain.Enums;

namespace TableTracker.Domain.Interfaces.Repositories
{
    public interface ICuisineRepository : IRepository<Cuisine, long>
    {
        Task<Cuisine> GetCuisineByName(string cuisineName);
    }
}
