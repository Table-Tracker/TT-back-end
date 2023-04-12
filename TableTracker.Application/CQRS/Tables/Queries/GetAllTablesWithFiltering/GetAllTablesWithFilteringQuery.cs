using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Tables.Queries.GetAllTablesWithFiltering
{
    public class GetAllTablesWithFilteringQuery : IRequest<TableDTO[]>
    {
        public GetAllTablesWithFilteringQuery(TableFilterModel tableFilterModel)
        {
            FilterModel = tableFilterModel;
        }

        public TableFilterModel FilterModel { get; set; }
    }
}
