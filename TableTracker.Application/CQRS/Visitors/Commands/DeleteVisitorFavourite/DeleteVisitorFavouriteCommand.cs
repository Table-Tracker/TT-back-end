using MediatR;

namespace TableTracker.Application.CQRS.Visitors.Commands.DeleteVisitorFavourite
{
    public class DeleteVisitorFavouriteCommand : IRequest<CommandResponse>
    {
        public DeleteVisitorFavouriteCommand(long visitorId, long restaurantId)
        {
            VisitorId = visitorId;
            RestaurantId = restaurantId;
        }

        public long VisitorId { get; set; }

        public long RestaurantId { get; set; }
    }
}
