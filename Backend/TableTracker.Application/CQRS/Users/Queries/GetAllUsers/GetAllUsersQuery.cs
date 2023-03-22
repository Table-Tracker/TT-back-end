using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Users.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<UserDTO[]>
    {
    }
}
