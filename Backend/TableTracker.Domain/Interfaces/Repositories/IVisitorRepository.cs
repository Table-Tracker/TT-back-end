using System.Collections.Generic;
using System.Threading.Tasks;

using TableTracker.Domain.Entities;

namespace TableTracker.Domain.Interfaces.Repositories
{
    public interface IVisitorRepository : IRepository<Visitor, long>
    {
        Task<ICollection<Visitor>> GetAllVisitorsByTrustFactor(float trustFactor);

        Task<ICollection<Visitor>> FilterVisitors(string filter);

        Task<ICollection<Restaurant>> FindVisitorFavouritesByVisitorId(long visitorId);
    }
}
