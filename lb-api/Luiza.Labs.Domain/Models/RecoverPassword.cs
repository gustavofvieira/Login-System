using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luiza.Labs.Domain.Models
{
    public class RecoverPassword
    {
        [BsonId]
        public Guid RecoverPasswordId { get; set; }
        public Guid UserId { get; set; }
        public string EmailAddress { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpirateAt { get; set; } = DateTime.UtcNow.AddMinutes(5);
    }
}
