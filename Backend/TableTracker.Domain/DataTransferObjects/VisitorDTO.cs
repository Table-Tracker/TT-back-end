using System;
using System.Collections.Generic;

namespace TableTracker.Domain.DataTransferObjects
{
    public class VisitorDTO
    {
        public long Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public float GeneralTrustFactor { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Location { get; set; }

        public ImageDTO Avatar { get; set; }


        public ICollection<ReservationDTO> Reservations { get; set; }

        public ICollection<RestaurantDTO> Favourites { get; set; }
    }
}
