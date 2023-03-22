using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Managers.Commands.UpdateManager
{
    public class UpdateManagerCommand : IRequest<CommandResponse<ManagerDTO>>
    {
        public UpdateManagerCommand(ManagerDTO managerDTO)
        {
            ManagerDTO = managerDTO;
        }

        public ManagerDTO ManagerDTO { get; set; }
    }
}
