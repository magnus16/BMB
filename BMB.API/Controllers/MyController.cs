using BMB.Entities.Models;
using BMB.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BMB.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MyController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IUserService _userService;
        private readonly IUserMovieService _userMovieService;

        public MyController(IMovieService movieService, IUserService userService, IUserMovieService userMovieService)
        {
            _movieService = movieService;
            _userService = userService;
            _userMovieService = userMovieService;
        }

        [HttpGet]
        [Route("{userId}")]
        public IActionResult Get(string userId)
        {
            var movies = _userMovieService.GetMoviesForUser(userId);
            return Ok(movies);
        }

        [HttpPost]
        [Route("{userId}/AddMovie/{movieId}")]
        public IActionResult AddMovie(string userId, string movieId)
        {
            _userMovieService.AddMovieToUserList(userId, movieId);
            return Ok();
        }

        [HttpPost]
        [Route("{userId}/RemoveMovie/{movieId}")]
        public IActionResult RemoveMovie(string userId, string movieId)
        {
            _userMovieService.RemoveMovieFromUserList(userId, movieId);
            return Ok();
        }

        [HttpPost]
        [Route("{userId}/Watched/{movieId}")]
        public IActionResult Watched(string userId, string movieId)
        {
            _userMovieService.ChangeMovieWatchStatus(userId, movieId, true);
            return Ok();
        }



    }
}
