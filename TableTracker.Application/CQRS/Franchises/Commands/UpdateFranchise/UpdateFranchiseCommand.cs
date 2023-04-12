using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Franchises.Commands.UpdateFranchise
{
    public class UpdateFranchiseCommand : IRequest<CommandResponse<FranchiseDTO>>
    {
        public UpdateFranchiseCommand(FranchiseDTO franchise)
        {
            Franchise = franchise;
        }

        public FranchiseDTO Franchise { get; set; }
    }
}
