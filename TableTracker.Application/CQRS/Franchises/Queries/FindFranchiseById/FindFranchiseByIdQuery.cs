using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Franchises.Queries.FindFranchiseById
{
    public class FindFranchiseByIdQuery : IRequest<FranchiseDTO>
    {
        public FindFranchiseByIdQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
