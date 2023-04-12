using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.RestaurantVisitors.Commands.UpdateRestaurantVisitor
{
    public class UpdateRestaurantVisitorCommand : IRequest<CommandResponse<RestaurantVisitorDTO>>
    {
        public UpdateRestaurantVisitorCommand(RestaurantVisitorDTO restaurantVisitor)
        {
            RestaurantVisitor = restaurantVisitor;
        }

        public RestaurantVisitorDTO RestaurantVisitor { get; set; }
    }
}
