using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommand : IRequest<CommandResponse<RestaurantDTO>>
    {
        public UpdateRestaurantCommand(RestaurantDTO restaurant)
        {
            Restaurant = restaurant;
        }

        public RestaurantDTO Restaurant { get; set; }
    }
}
