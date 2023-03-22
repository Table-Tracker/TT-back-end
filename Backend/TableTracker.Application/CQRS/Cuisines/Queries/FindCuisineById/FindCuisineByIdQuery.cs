using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Cuisines.Queries.FindCuisineById
{
    public class FindCuisineByIdQuery : IRequest<CuisineDTO>
    {
        public FindCuisineByIdQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
