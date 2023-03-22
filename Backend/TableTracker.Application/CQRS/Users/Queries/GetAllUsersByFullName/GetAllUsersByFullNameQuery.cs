using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Users.Queries.GetAllUsersByFullName
{
    public class GetAllUsersByFullNameQuery : IRequest<UserDTO[]>
    {
        public GetAllUsersByFullNameQuery(string fullName)
        {
            FullName = fullName;
        }

        public string FullName { get; set; }
    }
}
