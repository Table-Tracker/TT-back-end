using System.Collections.Generic;
using System.Threading.Tasks;

using TableTracker.Domain.Entities;

namespace TableTracker.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User, long>
    {
        Task<ICollection<User>> GetAllUsersByFullName(string name);

        Task<User> GetUserByEmail(string email);

        Task<ICollection<User>> FilterUsers(string filter);
    }
}
