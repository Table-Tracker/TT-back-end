using System.Collections.Generic;

using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Visitors.Queries.FindVisitorFavouritesByVisitorId
{
    public class FindVisitorFavouritesByVisitorIdQuery : IRequest<ICollection<RestaurantDTO>>
    {
        public FindVisitorFavouritesByVisitorIdQuery(long visitorId)
        {
            VisitorId = visitorId;
        }

        public long VisitorId { get; set; }
    }
}
