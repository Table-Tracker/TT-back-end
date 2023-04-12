namespace TableTracker.Domain.Entities
{
    public class Layout : IEntity<long>
    {
        public long Id { get; set; }
        public byte LayoutData { get; set; }

        public long RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
