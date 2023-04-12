using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Layout.Queries.FindLayoutByRestaurant
{
    public class FindLayoutByRestaurantQuery : IRequest<LayoutDTO>
    {
        public FindLayoutByRestaurantQuery(long restaurantDTO)
        {
            RestaurantDTO = restaurantDTO;
        }

        public long RestaurantDTO { get; set; }
    }
}
