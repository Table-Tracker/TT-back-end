using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Reservations.Commands.AddReservation
{
    public class AddReservationCommand : IRequest<CommandResponse<ReservationDTO>>
    {
        public AddReservationCommand(ReservationDTO reservation)
        {
            Reservation = reservation;
        }

        public ReservationDTO Reservation { get; set; }
    }
}
