using BMB.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMB.Services.Abstractions
{
    public interface IMovieService
    {
        List<Movie> GetAll();
        Movie GetById(string id);
        List<Movie> SearchByTitle(string query);
        void Add(Movie movie);
        void Update(Movie movie);
        void Delete(string id);
    }
}
