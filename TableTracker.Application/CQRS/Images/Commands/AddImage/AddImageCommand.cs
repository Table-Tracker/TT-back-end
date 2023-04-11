using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Images.Commands.AddImage
{
    public class AddImageCommand : IRequest<CommandResponse<ImageDTO>>
    {
    }
}
