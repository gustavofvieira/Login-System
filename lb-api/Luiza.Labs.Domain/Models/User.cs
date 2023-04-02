using MongoDB.Bson.Serialization.Attributes;

namespace Luiza.Labs.Domain.Models
{
    public class User
    {
        [BsonId]
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
    }
}
