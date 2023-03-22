using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Managers.Commands.DeleteManager
{
    public class DeleteManagerCommandHandler : IRequestHandler<DeleteManagerCommand, CommandResponse<ManagerDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteManagerCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandResponse<ManagerDTO>> Handle(DeleteManagerCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<IManagerRepository>();
            var entity = await repository.FindById(request.Id);

            if(await repository.Contains(entity))
            {
                repository.Remove(entity);
                await _unitOfWork.Save();

                return new CommandResponse<ManagerDTO>(_mapper.Map<ManagerDTO>(entity));
            }

            return new CommandResponse<ManagerDTO>(
                _mapper.Map<ManagerDTO>(entity),
                Domain.Enums.CommandResult.NotFound,
                "Could not find the given manager."
                );
        }
    }
}
