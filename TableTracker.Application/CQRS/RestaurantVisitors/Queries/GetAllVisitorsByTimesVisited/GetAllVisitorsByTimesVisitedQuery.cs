using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.RestaurantVisitors.Queries.GetAllVisitorsByTimesVisited
{
    public class GetAllVisitorsByTimesVisitedQuery : IRequest<RestaurantVisitorDTO[]>
    {
        public GetAllVisitorsByTimesVisitedQuery(int timesVisited, RestaurantDTO restaurant)
        {
            TimesVisited = timesVisited;
            Restaurant = restaurant;
        }

        public int TimesVisited { get; set; }

        public RestaurantDTO Restaurant { get; set; }
    }
}
