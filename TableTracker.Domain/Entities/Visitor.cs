using System.Collections.Generic;

namespace TableTracker.Domain.Entities
{
    public class Visitor : User
    {
        public float GeneralTrustFactor { get; set; }

        public ICollection<Reservation> Reservations { get; set; }

        public ICollection<Restaurant> Favourites { get; set; }
    }
}
