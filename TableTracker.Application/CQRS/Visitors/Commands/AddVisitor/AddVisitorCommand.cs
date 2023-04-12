using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Visitors.Commands.AddVisitor
{
    public class AddVisitorCommand : IRequest<CommandResponse<VisitorDTO>>
    {
        public AddVisitorCommand(VisitorDTO visitor)
        {
            Visitor = visitor;
        }

        public VisitorDTO Visitor { get; set; }
    }
}
