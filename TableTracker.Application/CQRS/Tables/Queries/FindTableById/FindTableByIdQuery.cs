using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Tables.Queries.FindTableById
{
    public class FindTableByIdQuery : IRequest<TableDTO>
    {
        public FindTableByIdQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
