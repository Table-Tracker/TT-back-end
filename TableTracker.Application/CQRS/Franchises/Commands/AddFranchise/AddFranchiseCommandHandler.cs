using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Domain.Enums;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Franchises.Commands.AddFranchise
{
    public class AddFranchiseCommandHandler : IRequestHandler<AddFranchiseCommand, CommandResponse<FranchiseDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public AddFranchiseCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandResponse<FranchiseDTO>> Handle(AddFranchiseCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Franchise>(request.Franchise);

            if (entity.Id != 0)
            {
                return new CommandResponse<FranchiseDTO>(
                    request.Franchise,
                    CommandResult.Failure,
                    "The franchise is already in the database");
            }

            await _unitOfWork.GetRepository<IFranchiseRepository>().Insert(entity);
            await _unitOfWork.Save();

            return new CommandResponse<FranchiseDTO>(_mapper.Map<FranchiseDTO>(entity));
        }
    }
}
