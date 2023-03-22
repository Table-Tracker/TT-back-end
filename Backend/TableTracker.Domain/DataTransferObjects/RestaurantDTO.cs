using System;
using System.Collections.Generic;

using TableTracker.Domain.Enums;

namespace TableTracker.Domain.DataTransferObjects
{
    public class RestaurantDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Rating { get; set; }

        public string PriceRange { get; set; }

        public int NumberOfTables { get; set; }

        public RestaurantType Type { get; set; }

        public Discount Discount { get; set; }

        public DateTime DateOfOpening { get; set; }


        public string Address { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Menu { get; set; }

        public string Website { get; set; }


        public ICollection<TableDTO> Tables { get; set; }

        public ICollection<CuisineDTO> Cuisines { get; set; }

        public ICollection<ImageDTO> Images { get; set; }


        public ImageDTO MainImage { get; set; }

        public FranchiseDTO Franchise { get; set; }

        public LayoutDTO Layout { get; set; }
    }
}
