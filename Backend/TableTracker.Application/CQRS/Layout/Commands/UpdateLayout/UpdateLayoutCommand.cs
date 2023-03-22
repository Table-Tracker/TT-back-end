using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Layout.Commands.UpdateLayout
{
    public class UpdateLayoutCommand : IRequest<CommandResponse<LayoutDTO>>
    {
        public UpdateLayoutCommand(LayoutDTO layoutDTO)
        {
            LayoutDTO = layoutDTO;
        }

        public LayoutDTO LayoutDTO { get; set; }
    }
}
