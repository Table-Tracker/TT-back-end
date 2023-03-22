using System;

using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.VisitorHistories.Queries.GetAllVisitsByDate
{
    public class GetAllVisitsByDateQuery : IRequest<VisitorHistoryDTO[]>
    {
        public GetAllVisitsByDateQuery(DateTime date)
        {
            Date = date;
        }

        public DateTime Date { get; set; }
    }
}
