using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Visitors.Queries.FindVisitorById
{
    public class FindVisitorByIdQuery : IRequest<VisitorDTO>
    {
        public FindVisitorByIdQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
