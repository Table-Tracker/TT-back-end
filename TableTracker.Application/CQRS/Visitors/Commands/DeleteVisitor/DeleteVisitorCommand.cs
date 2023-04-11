using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Visitors.Commands.DeleteVisitor
{
    public class DeleteVisitorCommand : IRequest<CommandResponse<VisitorDTO>>
    {
        public DeleteVisitorCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
