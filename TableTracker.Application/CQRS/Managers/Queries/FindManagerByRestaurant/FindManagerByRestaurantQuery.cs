using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Managers.Queries.FindManagerByRestaurant
{
    public class FindManagerByRestaurantQuery : IRequest<ManagerDTO>
    {
        public FindManagerByRestaurantQuery(long restaurantDTO)
        {
            RestaurantDTO = restaurantDTO;
        }

        public long RestaurantDTO { get; set; }
    }
}
