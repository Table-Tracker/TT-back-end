using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using TableTracker.Domain.Entities;

namespace TableTracker.Domain.Interfaces.Repositories
{
    public interface IVisitorHistoryRepository : IRepository<VisitorHistory, long>
    {
        Task<ICollection<VisitorHistory>> GetAllVisitsByDate(DateTime date);
    }
}
