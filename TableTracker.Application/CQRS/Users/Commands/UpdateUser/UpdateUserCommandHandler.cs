using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Domain.Enums;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, CommandResponse<UserDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandResponse<UserDTO>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<IUserRepository>();
            var entity = _mapper.Map<User>(request.User);

            if (await repository.Contains(entity))
            {
                repository.Update(entity);
                await _unitOfWork.Save();

                return new CommandResponse<UserDTO>(request.User);
            }

            return new CommandResponse<UserDTO>(
                request.User,
                CommandResult.NotFound,
                "Could not find the given user.");
        }
    }
}
