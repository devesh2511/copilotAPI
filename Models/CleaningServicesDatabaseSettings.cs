namespace API1.Models
{
    public class CleaningServicesDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string ServicesCollectionName { get; set; } = null!;
    }
}
