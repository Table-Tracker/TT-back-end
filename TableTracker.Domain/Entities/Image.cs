namespace TableTracker.Domain.Entities
{
    public class Image : IEntity<long>
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }
}
