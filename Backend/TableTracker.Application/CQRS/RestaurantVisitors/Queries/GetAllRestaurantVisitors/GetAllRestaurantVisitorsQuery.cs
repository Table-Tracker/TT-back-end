using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.RestaurantVisitors.Queries.GetAllRestaurantVisitors
{
    public class GetAllRestaurantVisitorsQuery : IRequest<RestaurantVisitorDTO[]>
    {
    }
}
