using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.VisitorHistories.Commands.AddVisit
{
    public class AddVisitCommand : IRequest<CommandResponse<VisitorHistoryDTO>>
    {
        public AddVisitCommand(VisitorHistoryDTO visit)
        {
            Visit = visit;
        }

        public VisitorHistoryDTO Visit { get; set; }
    }
}
