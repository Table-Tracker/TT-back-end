using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using TableTracker.Domain.Entities;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Infrastructure.Repositories
{
    public class UserRepository : Repository<User, long>, IUserRepository
    {
        public UserRepository(TableDbContext context)
            : base(context)
        {
        }

        public async Task<ICollection<User>> FilterUsers(string filter)
        {
            return await _context
                .Set<User>()
                .Where(x => x.FullName.Contains(filter) || x.Email.Contains(filter))
                .ToListAsync();
        }

        public async Task<ICollection<User>> GetAllUsersByFullName(string name)
        {
            return await _context
                .Set<User>()
                .Where(x => x.FullName == name)
                .ToListAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context
                .Set<User>()
                .FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
