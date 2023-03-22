using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Visitors.Queries.FilterVisitors
{
    public class FilterVisitorsQuery : IRequest<VisitorDTO[]>
    {
        public FilterVisitorsQuery(string filter)
        {
            Filter = filter;
        }

        public string Filter { get; set; }
    }
}
