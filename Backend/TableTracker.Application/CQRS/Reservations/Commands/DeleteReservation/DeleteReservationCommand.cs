using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Reservations.Commands.DeleteReservation
{
    public class DeleteReservationCommand : IRequest<CommandResponse<ReservationDTO>>
    {
        public DeleteReservationCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
