using System;

using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Reservations.Queries.GetAllReservationsByDate
{
    public class GetAllReservationsByDateQuery : IRequest<ReservationDTO[]>
    {
        public GetAllReservationsByDateQuery(long restaurantId, DateTime date)
        {
            RestaurantId = restaurantId;
            Date = date;
        }

        public long RestaurantId { get; set; }

        public DateTime Date { get; set; }
    }
}
