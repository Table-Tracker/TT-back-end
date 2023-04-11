using System;

namespace TableTracker.Domain.Entities
{
    public class VisitorHistory : IEntity<long>
    {
        public long Id { get; set; }
        public DateTime DateTime { get; set; }

        public long VisitorId { get; set; }
        public Visitor Visitor { get; set; }
        public long RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
