using System;
using System.Collections.Generic;

namespace TableTracker.Domain.Entities
{
    public class Reservation : IEntity<long>
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }

        public long VisitorId { get; set; }
        public Visitor Visitor { get; set; }

        public Table Table { get; set; }
        public long TableId { get; set; }
    }
}
