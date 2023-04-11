using System.Collections.Generic;

using TableTracker.Domain.Enums;

namespace TableTracker.Domain.DataTransferObjects
{
    public class CuisineDTO
    {
        public long Id { get; set; }

        public string Cuisine { get; set; }

        public ICollection<RestaurantDTO> Restaurants { get; set; }
    }
}
