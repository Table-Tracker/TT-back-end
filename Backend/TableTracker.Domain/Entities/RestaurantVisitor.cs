namespace TableTracker.Domain.Entities
{
    public class RestaurantVisitor : IEntity<long>
    {
        public long Id { get; set; }
        public float RestaurantRate { get; set; }
        public int TimesVisited { get; set; }
        public double AverageMoneySpent { get; set; }

        public long RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        public long VisitorId { get; set; }
        public Visitor Visitor { get; set; }
    }
}
