using System.Collections.Generic;

using TableTracker.Domain.Enums;

namespace TableTracker.Domain.DataTransferObjects
{
    public class RestaurantsFilterModel
    {
        public float? Rating { get; set; }

        public IEnumerable<CuisineDTO> Cuisines { get; set; }

        public int Price { get; set; }

        public RestaurantType? Type { get; set; }

        public Discount? Discount { get; set; }

        public FranchiseDTO Franchise { get; set; }
    }
}
