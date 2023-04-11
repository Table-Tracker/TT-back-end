using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Visitors.Queries.GetAllVisitors
{
    public class GetAllVisitorsQuery : IRequest<VisitorDTO[]>
    {
    }
}
