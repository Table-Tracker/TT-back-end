using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Layout.Commands.AddLayout
{
    public class AddLayoutCommand : IRequest<CommandResponse<LayoutDTO>>
    {
        public AddLayoutCommand(LayoutDTO layoutDTO)
        {
            LayoutDTO = layoutDTO;
        }

        public LayoutDTO LayoutDTO { get; set; }
    }
}
