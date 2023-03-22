using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.RestaurantVisitors.Commands.AddRestaurantVisitor
{
    public class AddRestaurantVisitorCommand : IRequest<CommandResponse<RestaurantVisitorDTO>>
    {
        public AddRestaurantVisitorCommand(RestaurantVisitorDTO restaurantVisitor)
        {
            RestaurantVisitor = restaurantVisitor;
        }

        public RestaurantVisitorDTO RestaurantVisitor { get; set; }
    }
}
