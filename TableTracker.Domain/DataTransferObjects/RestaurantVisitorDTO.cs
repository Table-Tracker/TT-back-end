namespace TableTracker.Domain.DataTransferObjects
{
    public class RestaurantVisitorDTO
    {
        public long Id { get; set; }

        public float RestaurantRate { get; set; }

        public int TimesVisited { get; set; }

        public double AverageMoneySpent { get; set; }

        public RestaurantDTO Restaurant { get; set; }

        public VisitorDTO Visitor { get; set; }
    }
}
