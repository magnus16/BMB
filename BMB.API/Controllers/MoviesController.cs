
using BMB.Entities.Models;
using BMB.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BMB.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var movies = _movieService.GetAll();
            return Ok(movies);
        }

        [HttpPost]
        public IActionResult Add(Movie movie)
        {
            _movieService.Add(movie);
            return Ok(new
            {
                Message = $"Movie {movie.Title} has been added."
            });
        }
    }
}
