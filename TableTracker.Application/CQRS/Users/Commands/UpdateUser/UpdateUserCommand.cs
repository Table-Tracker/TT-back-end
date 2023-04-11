using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<CommandResponse<UserDTO>>
    {
        public UpdateUserCommand(UserDTO user)
        {
            User = user;
        }

        public UserDTO User { get; set; }
    }
}
