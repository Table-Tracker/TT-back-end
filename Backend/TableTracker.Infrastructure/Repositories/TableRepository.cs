using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using TableTracker.Domain.Entities;
using TableTracker.Domain.Enums;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Infrastructure.Repositories
{
    public class TableRepository : Repository<Table, long>, ITableRepository
    {
        public TableRepository(TableDbContext context)
            : base(context)
        {
        }

        public async Task<ICollection<Table>> GetAllTablesWithFiltering(
            Restaurant restaurant,
            int? numberOfSeats = null,
            double? tableSize = null,
            int? floor = null,
            DateTime? reserveDate = null,
            TableState? state = null)
        {
            return await _context
                .Set<Table>()
                .Include(x => x.Reservations)
                .Include(x => x.Restaurant)
                .Where(x => x.RestaurantId == restaurant.Id)
                .Where(x => !numberOfSeats.HasValue || x.NumberOfSeats == numberOfSeats)
                .Where(x => !tableSize.HasValue || x.TableSize - tableSize < 0.00001)
                .Where(x => !floor.HasValue || x.Floor == floor)
                .Where(x => !state.HasValue || x.State == state)
                .ToListAsync();
        }

        public async Task<ICollection<Table>> GetAllTablesByRestaurant(long restaurantId)
        {
            return await _context
                .Set<Table>()
                .Include(x => x.Reservations)
                .Include(x => x.Restaurant)
                .Where(x => x.RestaurantId == restaurantId)
                .ToListAsync();
        }
    }
}
