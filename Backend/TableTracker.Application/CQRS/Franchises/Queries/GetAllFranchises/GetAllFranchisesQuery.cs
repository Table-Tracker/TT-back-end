using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Franchises.Queries.GetAllFranchises
{
    public class GetAllFranchisesQuery : IRequest<FranchiseDTO[]>
    {
    }
}
