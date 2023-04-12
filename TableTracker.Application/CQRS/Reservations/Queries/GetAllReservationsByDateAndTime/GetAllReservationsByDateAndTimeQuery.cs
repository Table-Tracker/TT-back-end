using System;

using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Reservations.Queries.GetAllReservationsByDateAndTime
{
    public class GetAllReservationsByDateAndTimeQuery : IRequest<ReservationDTO[]>
    {
        public GetAllReservationsByDateAndTimeQuery(long restaurantId, DateTime date)
        {
            RestaurantId = restaurantId;
            Date = date;
        }

        public DateTime Date { get; set; }

        public long RestaurantId { get; set; }
    }
}
