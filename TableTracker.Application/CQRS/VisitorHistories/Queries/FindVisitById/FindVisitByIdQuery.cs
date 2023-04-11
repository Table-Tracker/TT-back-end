using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.VisitorHistories.Queries.FindVisitById
{
    public class FindVisitByIdQuery : IRequest<VisitorHistoryDTO>
    {
        public FindVisitByIdQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
