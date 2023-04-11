using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Reservations.Queries.GetAllReservations
{
    public class GetAllReservationsQuery : IRequest<ReservationDTO[]>
    {
        public GetAllReservationsQuery(long restaurantId)
        {
            RestaurantId = restaurantId;
        }

        public long RestaurantId { get; set; }
    }
}
