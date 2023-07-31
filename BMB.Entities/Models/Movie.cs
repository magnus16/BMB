using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMB.Entities.Models
{

    [BsonIgnoreExtraElements]
    public class Movie : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? Genre { get; set; }
        public string? ImageURL { get; set; }
    }
}
