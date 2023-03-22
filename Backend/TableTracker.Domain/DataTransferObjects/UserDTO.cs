using System;

namespace TableTracker.Domain.DataTransferObjects
{
    public class UserDTO
    {
        public long Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Location { get; set; }

        public DateTime DateOfBirth { get; set; }

        public ImageDTO Avatar { get; set; }
    }
}
