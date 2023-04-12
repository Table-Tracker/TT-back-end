using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Domain.Enums;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Franchises.Commands.UpdateFranchise
{
    public class UpdateFranchiseCommandHandler : IRequestHandler<UpdateFranchiseCommand, CommandResponse<FranchiseDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateFranchiseCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandResponse<FranchiseDTO>> Handle(UpdateFranchiseCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<IFranchiseRepository>();
            var entity = _mapper.Map<Franchise>(request.Franchise);

            if (await repository.Contains(entity))
            {
                repository.Update(entity);
                await _unitOfWork.Save();

                return new CommandResponse<FranchiseDTO>(request.Franchise);
            }

            return new CommandResponse<FranchiseDTO>(
                request.Franchise,
                CommandResult.NotFound,
                "Could not find the given restaurant.");
        }
    }
}
