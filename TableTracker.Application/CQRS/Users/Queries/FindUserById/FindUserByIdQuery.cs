using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Users.Queries.FindUserById
{
    public class FindUserByIdQuery : IRequest<UserDTO>
    {
        public FindUserByIdQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
