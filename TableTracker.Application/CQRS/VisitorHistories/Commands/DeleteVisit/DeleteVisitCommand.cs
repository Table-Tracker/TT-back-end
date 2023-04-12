using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.VisitorHistories.Commands.DeleteVisit
{
    public class DeleteVisitCommand : IRequest<CommandResponse<VisitorHistoryDTO>>
    {
        public DeleteVisitCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
