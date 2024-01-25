using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API1.Models
{

    public class Reviews
    {
        [BsonElement("Id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; internal set; }

        [BsonElement("Name")]
        public string? ReviewId { get; set; } = null;
        public string UserName { get; set; } = null!;

        public string BookingId { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string Review { get; set; } = null!;

    }
}
