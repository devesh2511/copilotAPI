using MongoDB.Bson.Serialization.Attributes;
using System.Numerics;

namespace API1.Models
{
    public class User
    {
        public string Username { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public long Phone { get; set; }
        //public Booking[] Bookings { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; internal set; }
    }
}
