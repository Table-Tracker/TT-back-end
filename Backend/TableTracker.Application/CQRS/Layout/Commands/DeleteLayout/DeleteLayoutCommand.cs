using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Layout.Commands.DeleteLayout
{
    public class DeleteLayoutCommand : IRequest<CommandResponse<LayoutDTO>>
    {
        public DeleteLayoutCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
