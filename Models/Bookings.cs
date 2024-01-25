using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System.Security.Cryptography;
using ThirdParty.Json.LitJson;

namespace API1.Models
{

    public class Bookings
    {
        [BsonElement("BookingId")]
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string BookingId { get; set; }

        [BsonElement("Name")]
        public string ServiceName { get; set; } = null!;

        public string ServiceId { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string PaymentId { get; set; } = null!;

        public int Price { get; set; }

        public string Category { get; set; } = null!;

    }
}
