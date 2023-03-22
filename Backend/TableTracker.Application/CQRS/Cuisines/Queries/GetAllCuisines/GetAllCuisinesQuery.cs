using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Cuisines.Queries.GetAllCuisines
{
    public class GetAllCuisinesQuery : IRequest<CuisineDTO[]>
    {
    }
}
