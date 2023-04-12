using System;
using System.Collections.Generic;

using TableTracker.Domain.Enums;

namespace TableTracker.Domain.Entities
{
    public class Restaurant : IEntity<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Rating { get; set; }
        public int PriceRange { get; set; }
        public int NumberOfTables { get; set; }
        public RestaurantType Type { get; set; }
        public Discount Discount { get; set; }
        public DateTime DateOfOpening { get; set; }

        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Menu { get; set; }
        public string Website { get; set; }

        public ICollection<Table> Tables { get; set; }
        public ICollection<Cuisine> Cuisines { get; set; }
        public ICollection<Image> Images { get; set; }

        public long? MainImageId { get; set; }
        public Image MainImage { get; set; }
        public long FranchiseId { get; set; }
        public Franchise Franchise { get; set; }
        public long? LayoutId { get; set; }
        public Layout Layout { get; set; }
        public long? ManagerId { get; set; }
        public Manager Manager { get; set; }
    }
}
