using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Tables.Queries.GetAllTablesByRestaurant
{
    public class GetAllTablesByRestaurantQuery : IRequest<TableDTO[]>
    {
        public GetAllTablesByRestaurantQuery(long restaurantId)
        {
            RestaurantID = restaurantId;
        }

        public long RestaurantID { get; set; }

    }
}
