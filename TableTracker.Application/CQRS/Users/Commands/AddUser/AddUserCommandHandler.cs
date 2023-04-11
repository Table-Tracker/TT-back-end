using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Domain.Enums;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Users.Commands.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, CommandResponse<UserDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public AddUserCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandResponse<UserDTO>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<User>(request.User);

            if (entity.Id != 0)
            {
                return new CommandResponse<UserDTO>(
                    request.User,
                    CommandResult.Failure,
                    "The user is already in the database");
            }

            await _unitOfWork
                .GetRepository<IUserRepository>()
                .Insert(entity);

            await _unitOfWork.Save();

            return new CommandResponse<UserDTO>(_mapper.Map<UserDTO>(entity));
        }
    }
}
