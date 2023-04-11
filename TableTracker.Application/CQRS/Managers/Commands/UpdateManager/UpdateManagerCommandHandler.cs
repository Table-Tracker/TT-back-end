using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Managers.Commands.UpdateManager
{
    public class UpdateManagerCommandHandler : IRequestHandler<UpdateManagerCommand, CommandResponse<ManagerDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateManagerCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandResponse<ManagerDTO>> Handle(UpdateManagerCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<IManagerRepository>();
            var entity = _mapper.Map<Manager>(request.ManagerDTO);

            if(await repository.Contains(entity))
            {
                entity.Restaurant = null;

                repository.Update(entity);
                await _unitOfWork.Save();

                return new CommandResponse<ManagerDTO>(request.ManagerDTO);
            }

            return new CommandResponse<ManagerDTO>(
                request.ManagerDTO,
                Domain.Enums.CommandResult.NotFound,
                "Could not find the given manager."
                );
        }
    }
}
