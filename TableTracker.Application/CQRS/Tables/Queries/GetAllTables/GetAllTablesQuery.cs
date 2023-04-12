using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Tables.Queries.GetAllTables
{
    public class GetAllTablesQuery : IRequest<TableDTO[]>
    {
    }
}
