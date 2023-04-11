using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.VisitorHistories.Queries.GetAllVisits
{
    public class GetAllVisitsQuery : IRequest<VisitorHistoryDTO[]>
    {
    }
}
