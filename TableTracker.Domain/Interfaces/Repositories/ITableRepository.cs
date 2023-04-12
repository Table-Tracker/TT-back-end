using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using TableTracker.Domain.Entities;
using TableTracker.Domain.Enums;

namespace TableTracker.Domain.Interfaces.Repositories
{
    public interface ITableRepository : IRepository<Table, long>
    {
        Task<ICollection<Table>> GetAllTablesWithFiltering(
            Restaurant restaurant,
            int? numberOfSeats = null,
            double? tableSize = null,
            int? floor = null,
            DateTime? reserveDate = null,
            TableState? state = null);

        Task<ICollection<Table>> GetAllTablesByRestaurant(long restaurantId);
    }
}
