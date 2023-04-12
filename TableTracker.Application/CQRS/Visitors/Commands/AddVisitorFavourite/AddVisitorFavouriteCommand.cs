using MediatR;

namespace TableTracker.Application.CQRS.Visitors.Commands.AddVisitorFavourite
{
    public class AddVisitorFavouriteCommand : IRequest<CommandResponse>
    {
        public AddVisitorFavouriteCommand(long visitorId, long restaurantId)
        {
            VisitorId = visitorId;
            RestaurantId = restaurantId;
        }

        public long VisitorId { get; set; }

        public long RestaurantId { get; set; }
    }
}
