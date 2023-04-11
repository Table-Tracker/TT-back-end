using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.VisitorHistories.Commands.UpdateVisit
{
    public class UpdateVisitCommand : IRequest<CommandResponse<VisitorHistoryDTO>>
    {
        public UpdateVisitCommand(VisitorHistoryDTO visit)
        {
            Visit = visit;
        }

        public VisitorHistoryDTO Visit { get; set; }
    }
}
