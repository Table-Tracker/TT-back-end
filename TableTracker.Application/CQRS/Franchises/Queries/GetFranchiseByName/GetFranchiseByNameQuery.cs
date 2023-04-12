using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Franchises.Queries.GetFranchiseByName
{
    public class GetFranchiseByNameQuery : IRequest<FranchiseDTO>
    {
        public GetFranchiseByNameQuery(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
