using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Managers.Queries.FindManagerById
{
    public class FindManagerByIdQuery : IRequest<ManagerDTO>
    {
        public FindManagerByIdQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
