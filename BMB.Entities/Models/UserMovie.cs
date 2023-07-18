using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMB.Entities.Models
{
    public class UserMovie
    {
        public string MovieId { get; set; } = string.Empty;
        public bool HasWatched { get; set; } = false;
        public DateTime? WatchedDate { get; set; }
    }
}
