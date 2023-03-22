using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using TableTracker.Domain.Entities;

namespace TableTracker.Domain.Interfaces.Repositories
{
    public interface IReservationRepository : IRepository<Reservation, long>
    {
        Task<ICollection<Reservation>> GetAllReservations(long restaurantId);

        Task<ICollection<Reservation>> GetAllReservationsByDate(long restaurantId, DateTime date);

        Task<ICollection<Reservation>> GetAllReservationsByDateAndTime(long restaurantId, DateTime date);

        Task<ICollection<Reservation>> GetAllReservationsForTable(long tableId, DateTime? date = null);
    }
}
