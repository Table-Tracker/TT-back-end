using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.RestaurantVisitors.Queries.GetAllVisitorsByAverageMoneySpent
{
    public class GetAllVisitorsByAverageMoneySpentQuery : IRequest<RestaurantVisitorDTO[]>
    {
        public GetAllVisitorsByAverageMoneySpentQuery(
            double averageMoneySpent,
            RestaurantDTO restaurant)
        {
            AverageMoneySpent = averageMoneySpent;
            Restaurant = restaurant;
        }

        public double AverageMoneySpent { get; set; }

        public RestaurantDTO Restaurant { get; set; }
    }
}
