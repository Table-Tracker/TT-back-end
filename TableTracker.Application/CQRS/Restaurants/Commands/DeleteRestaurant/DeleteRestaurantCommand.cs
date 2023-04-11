using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommand : IRequest<CommandResponse<RestaurantDTO>>
    {
        public DeleteRestaurantCommand(long restaurantId)
        {
            RestaurantId = restaurantId;
        }

        public long RestaurantId { get; set; }
    }
}
