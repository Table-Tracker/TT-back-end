using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.RestaurantVisitors.Commands.DeleteRestaurantVisitor
{
    public class DeleteRestaurantVisitorCommand : IRequest<CommandResponse<RestaurantVisitorDTO>>
    {
        public DeleteRestaurantVisitorCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
