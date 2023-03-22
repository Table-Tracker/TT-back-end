using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Layout.Queries.FindLayoutById
{
    public class FindLayoutByIdQuery : IRequest<LayoutDTO>
    {
        public FindLayoutByIdQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
