using System;

namespace TableTracker.Domain.Entities
{
    public class User : IEntity<long>
    {
        public long Id { get; set; } 

        public string FullName { get; set; }

        //public UserRole Role { get; set; }

        public string Email { get; set; }

        public string Location { get; set; }

        public DateTime DateOfBirth { get; set; }


        public long? AvatarId { get; set; }

        public Image Avatar { get; set; }
    }
}
