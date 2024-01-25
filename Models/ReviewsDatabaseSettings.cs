namespace API1.Models
{
    public class ReviewsDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string ReviewsCollectionName { get; set; } = null!;
    }
}
