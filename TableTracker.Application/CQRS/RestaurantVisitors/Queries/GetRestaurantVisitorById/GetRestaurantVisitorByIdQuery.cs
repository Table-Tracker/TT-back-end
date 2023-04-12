using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.RestaurantVisitors.Queries.GetRestaurantVisitorById
{
    public class GetRestaurantVisitorByIdQuery : IRequest<RestaurantVisitorDTO>
    {
        public GetRestaurantVisitorByIdQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
