using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Enums;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Franchises.Commands.DeleteFranchise
{
    public class DeleteFranchiseCommandHandler : IRequestHandler<DeleteFranchiseCommand, CommandResponse<FranchiseDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteFranchiseCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandResponse<FranchiseDTO>> Handle(DeleteFranchiseCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<IFranchiseRepository>();
            var entity = await repository.FindById(request.Id);

            if (await repository.Contains(entity))
            {
                repository.Remove(entity);
                await _unitOfWork.Save();

                return new CommandResponse<FranchiseDTO>(_mapper.Map<FranchiseDTO>(entity));
            }

            return new CommandResponse<FranchiseDTO>(
                _mapper.Map<FranchiseDTO>(entity),
                CommandResult.NotFound,
                "Could not find the given franchise.");
        }
    }
}
