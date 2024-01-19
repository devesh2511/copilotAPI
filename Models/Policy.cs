using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API1.Models
{
    public class Policy
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }
        public required string PolicyNumber { get; set; }
        public int Duration { get; set; }
        public required string ValidForAge { get; set; }
        public required string ValidForGender { get; set; }
        public decimal Premium { get; set; }
        public required string BenefitLine { get; set; }
        public required string Coverage { get; set; }
    }
}