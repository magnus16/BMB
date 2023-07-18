using BMB.Data.Abstractions;
using BMB.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BMB.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepo;
        public MoviesController(IMovieRepository movieRepo)
        {
            _movieRepo = movieRepo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var movies = _movieRepo.GetAll();
            return Ok(movies);
        }

        [HttpPost]
        public IActionResult Add(Movie movie)
        {
            _movieRepo.Create(movie);
            return Ok(new
            {
                Message = $"Movie {movie.Title} has been added."
            });
        }
    }
}
