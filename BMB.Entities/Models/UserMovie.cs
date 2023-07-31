using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMB.Entities.Models
{
    public class UserMovie : BaseEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = string.Empty;
        [BsonRepresentation(BsonType.ObjectId)]
        public string MovieId { get; set; } = string.Empty;
        public bool Watched { get; set; } = false;
        public DateTime? WatchedOn { get; set; }
        public double? Rating { get; set; }
    }
}
