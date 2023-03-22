using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Visitors.Commands.AddAvatar
{
    public class AddAvatarCommand : IRequest<CommandResponse<ImageDTO>>
    {
        public AddAvatarCommand(long visitorId, string fileName)
        {
            VisitorId = visitorId;
            FileName = fileName;
        }

        public long VisitorId { get; set; }

        public string FileName { get; set; }
    }
}
