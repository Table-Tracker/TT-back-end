using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Managers.Commands.AddManager
{
    public class AddManagerCommand : IRequest<CommandResponse<ManagerDTO>>
    {
        public AddManagerCommand(ManagerDTO managerDTO)
        {
            ManagerDTO = managerDTO;
        }

        public ManagerDTO ManagerDTO { get; set; }
    }
}
