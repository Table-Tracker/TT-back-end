using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Visitors.Commands.UpdateVisitor
{
    public class UpdateVisitorCommand : IRequest<CommandResponse<VisitorDTO>>
    {
        public UpdateVisitorCommand(VisitorDTO visitor)
        {
            Visitor = visitor;
        }

        public VisitorDTO Visitor { get; set; }
    }
}
