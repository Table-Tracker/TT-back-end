using System;

using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Reservations.Queries.GetAllReservationsForTable
{
    public class GetAllReservationsForTableQuery : IRequest<ReservationDTO[]>
    {
        public GetAllReservationsForTableQuery(long id, DateTime date)
        {
            Date = date;
            Id = id;
        }

        public DateTime Date { get; set; }
        public long Id { get; set; }
    }
}
