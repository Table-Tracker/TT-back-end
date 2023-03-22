using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Users.Commands.AddUser
{
    public class AddUserCommand : IRequest<CommandResponse<UserDTO>>
    {
        public AddUserCommand(UserDTO user)
        {
            User = user;
        }

        public UserDTO User { get; set; }
    }
}
