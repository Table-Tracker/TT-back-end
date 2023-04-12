using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<CommandResponse<UserDTO>>
    {
        public DeleteUserCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
