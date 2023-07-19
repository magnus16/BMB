using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMB.Entities.DTO
{
    public class UserMovieDTO
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public double Rating { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? Genre { get; set; }
        public string? ImageURL { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string MovieId { get; set; } = string.Empty;
        public bool Watched { get; set; } = false;
        public DateTime? WatchedOn { get; set; }
    }
}
