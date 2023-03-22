using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Reservations.Queries.FindReservationById
{
    public class FindReservationByIdQuery : IRequest<ReservationDTO>
    {
        public FindReservationByIdQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
