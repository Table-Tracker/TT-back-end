using System;
using System.Collections.Generic;

namespace TableTracker.Domain.DataTransferObjects
{
    public class ReservationDTO
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public TableDTO Table { get; set; }

        public VisitorDTO Visitor { get; set; }
    }
}
