using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Tables.Commands.DeleteTable
{
    public class DeleteTableCommand : IRequest<CommandResponse<TableDTO>>
    {
        public DeleteTableCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
