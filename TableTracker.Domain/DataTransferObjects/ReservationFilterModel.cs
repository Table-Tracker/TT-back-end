using System;

namespace TableTracker.Domain.DataTransferObjects
{
    public class ReservationFilterModel
    {
        public TableDTO Table { get; set; }

        public DateTime? Date { get; set; }    
    }
}
