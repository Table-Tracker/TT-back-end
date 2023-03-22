using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Tables.Commands.UpdateTable
{
    public class UpdateTableCommand : IRequest<CommandResponse<TableDTO>>
    {
        public UpdateTableCommand(TableDTO table)
        {
            Table = table;
        }

        public TableDTO Table { get; set; }
    }
}
