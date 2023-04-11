using System;

namespace TableTracker.Domain.DataTransferObjects
{
    public class VisitorHistoryDTO
    {
        public long Id { get; set; }

        public DateTime DateTime { get; set; }

        public VisitorDTO Visitor { get; set; }

        public RestaurantDTO Restaurant { get; set; }
    }
}
