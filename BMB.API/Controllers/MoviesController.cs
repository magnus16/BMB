
using BMB.Entities.Models;
using BMB.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BMB.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
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

        [HttpGet]
        [Route("{movieId}")]
        public IActionResult Get(string movieId)
        {
            var movie = _movieService.GetById(movieId);
            return Ok(movie);
        }

        [HttpPost]
        [Route("New")]
        public IActionResult Add(Movie movie)
        {
            _movieService.Add(movie);
            return Ok(new
            {
                Message = $"Movie {movie.Title} has been added."
            });
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update(Movie movie)
        {
            _movieService.Update(movie);
            return Ok(new
            {
                Message = $"Movie {movie.Title} has been updateds."
            });
        }





    }
}
