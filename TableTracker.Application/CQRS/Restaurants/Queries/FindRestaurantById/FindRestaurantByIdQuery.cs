using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Restaurants.Queries.FindRestaurantById
{
    public class FindRestaurantByIdQuery : IRequest<RestaurantDTO>
    {
        public FindRestaurantByIdQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
