using TableTracker.Domain.Enums;

namespace TableTracker.Domain.DataTransferObjects
{
    public class ManagerDTO
    {
        public long Id { get; set; }

        public string FullName { get; set; }

        public string Avatar { get; set; }

        public string Email { get; set; }

        public ManagerState ManagerState { get; set; }

        public RestaurantDTO Restaurant { get; set; }
    }
}
