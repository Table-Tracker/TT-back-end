using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Franchises.Commands.AddFranchise
{
    public class AddFranchiseCommand : IRequest<CommandResponse<FranchiseDTO>>
    {
        public AddFranchiseCommand(FranchiseDTO franchise)
        {
            Franchise = franchise;
        }

        public FranchiseDTO Franchise { get; set; }
    }
}
