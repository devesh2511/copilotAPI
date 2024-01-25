namespace API1.Models
{
    public class BookingsDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string BookingsCollectionName { get; set; } = null!;
    }
}
