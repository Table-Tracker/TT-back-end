using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Reservations.Commands.UpdateReservation
{
    public class UpdateReservationCommand : IRequest<CommandResponse<ReservationDTO>>
    {
        public UpdateReservationCommand(ReservationDTO reservation)
        {
            Reservation = reservation;
        }

        public ReservationDTO Reservation { get; set; }
    }
}
