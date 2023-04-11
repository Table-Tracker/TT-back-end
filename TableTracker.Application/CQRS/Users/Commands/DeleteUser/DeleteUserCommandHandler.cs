using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Enums;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, CommandResponse<UserDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteUserCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandResponse<UserDTO>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<IUserRepository>();
            var entity = await repository.FindById(request.Id);

            if (await repository.Contains(entity))
            {
                repository.Remove(entity);
                await _unitOfWork.Save();

                return new CommandResponse<UserDTO>(_mapper.Map<UserDTO>(entity));
            }

            return new CommandResponse<UserDTO>(
                _mapper.Map<UserDTO>(entity),
                CommandResult.NotFound,
                "Could not find the given user.");
        }
    }
}
