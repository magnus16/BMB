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
        public string? query { get; set; } = null;
        public string? sortBy { get; set; } = null;
        public string sort { get; set; } = "desc";
        public int pageSize { get; set; } = 100;
        public int pageNumber { get; set; } = 1;
        public string? userId { get; set; } = string.Empty;
    }

    public class MovieSearchParams : SearchParams
    {
        public Enums.GenreType? Genre { get; set; } = null;
        public int? Year { get; set; } = null;
    }

}
