using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Users.Queries.GetUserByEmail
{
    public class GetUserByEmailQuery : IRequest<UserDTO>
    {
        public GetUserByEmailQuery(string email)
        {
            Email = email;
        }

        public string Email { get; set; }
    }
}
