using MediatR;

namespace TableTracker.Application.CQRS.Visitors.Commands.DeleteAvatar
{
    public class DeleteAvatarCommand : IRequest<CommandResponse<string>>
    {
        public DeleteAvatarCommand(long visitorId)
        {
            VisitorId = visitorId;
        }

        public long VisitorId { get; set; }
    }
}
