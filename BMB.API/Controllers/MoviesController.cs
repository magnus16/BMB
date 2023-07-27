
using BMB.API.Extensions;
using BMB.Entities.DTO;
using BMB.Entities.Models;
using BMB.Services;
using BMB.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BMB.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }



        [HttpGet]
        public IActionResult Get()
        {
            MovieSearchParams movieSearchParams = new MovieSearchParams();
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                movieSearchParams.userId = User.Identity.GetUserId();
            }
            List<UserMovieDTO> movies = _movieService.Get(movieSearchParams);
            return Ok(movies);
        }
        [HttpPost]
        [Route("Search")]
        [HttpGet]
        public IActionResult Search(MovieSearchParams? searchParams = null)
        {
            if (searchParams == null)
            {
                searchParams = new MovieSearchParams();
            }
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                searchParams.userId = User.Identity.GetUserId();
            }
            List<UserMovieDTO> movies = _movieService.Get(searchParams);
            return Ok(movies);
        }


        [HttpGet]
        [Route("Details/{movieId}")]
        public IActionResult Details(string movieId)
        {
            if (string.IsNullOrEmpty(movieId))
            {
                throw new ArgumentNullException("movieId");
            }
            var movie = _movieService.GetById(movieId);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }


        [HttpPost]
        [Route("New")]
        public IActionResult Add(Movie movie)
        {
            if (movie == null)
            {
                return BadRequest();
            }
            if (string.IsNullOrEmpty(movie.Title))
            {
                return BadRequest("Movie title is required");
            }
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
            if (movie == null)
            {
                return BadRequest();
            }
            if (string.IsNullOrEmpty(movie.Id))
            {
                return BadRequest("Movie id is required");
            }
            if (string.IsNullOrEmpty(movie.Title))
            {
                return BadRequest("Movie title is required");
            }
            _movieService.Update(movie);
            return Ok(new
            {
                Message = $"Movie {movie.Title} has been updateds."
            });
        }

        [HttpDelete]
        [Route("Delete/{movieId}")]
        public IActionResult Delete(string movieId)
        {
            if (string.IsNullOrEmpty(movieId))
            {
                return BadRequest("Movie id is required");
            }
            _movieService.Delete(movieId);
            return Ok(new
            {
                Message = $"Movie with id#{movieId} has been updateds."
            });
        }





    }
}
