using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Users.Queries.FilterUsers
{
    public class FilterUsersQuery : IRequest<UserDTO[]>
    {
        public FilterUsersQuery(string filter)
        {
            Filter = filter;
        }

        public string Filter { get; set; }
    }
}
