using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Managers.Commands.AddManager
{
    public class AddManagerCommandHandler : IRequestHandler<AddManagerCommand, CommandResponse<ManagerDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public AddManagerCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandResponse<ManagerDTO>> Handle(AddManagerCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Manager>(request.ManagerDTO);

            if(entity.Id != 0)
            {
                return new CommandResponse<ManagerDTO>(
                    request.ManagerDTO,
                    Domain.Enums.CommandResult.Failure,
                    "The manager is already in the database.");
            }

            entity.Restaurant = null;

            await _unitOfWork.GetRepository<IManagerRepository>().Insert(entity);
            await _unitOfWork.Save();

            return new CommandResponse<ManagerDTO>(request.ManagerDTO);
        }
    }
}
