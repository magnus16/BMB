using BMB.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMB.Entities.DTO
{
    public class SearchParams
    {
        public string? searchQuery { get; set; }
        public string? sortBy { get; set; }
        public string sort { get; set; } = "desc";
        public int pageSize { get; set; } = 10;
        public int pageNumber { get; set; } = 1;
    }

    public class MovieSearchParams : SearchParams
    {
        public Enums.GenreType? GenreType { get; set; }
        public int? Year { get; set; }
    }

}
