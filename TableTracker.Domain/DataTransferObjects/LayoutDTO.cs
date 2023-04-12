namespace TableTracker.Domain.DataTransferObjects
{
    public class LayoutDTO
    {
        public long Id { get; set; }

        public byte LayoutData { get; set; }

        public RestaurantDTO Restaurant { get; set; }
    }
}
