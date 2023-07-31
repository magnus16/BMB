using BMB.Entities.Models;
using BMB.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BMB.API.Extensions.IdentityExtension;

namespace BMB.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MyController : ControllerBase
    {
        private readonly IUserMovieService _userMovieService;

        public MyController(IUserMovieService userMovieService)
        {
            _userMovieService = userMovieService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var movies = _userMovieService.GetMoviesForUser(userId);
                return Ok(movies);
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("AddMovie/{movieId}")]
        public IActionResult AddMovie(string movieId)
        {
            if (string.IsNullOrEmpty(movieId))
            {
                return BadRequest("Movie Id is required");
            }
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                _userMovieService.AddMovieToUserList(userId, movieId);
                return Ok();
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("RemoveMovie/{movieId}")]
        public IActionResult RemoveMovie(string movieId)
        {
            if (string.IsNullOrEmpty(movieId))
            {
                return BadRequest("Movie Id is required");
            }
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                _userMovieService.RemoveMovieFromUserList(userId, movieId);
                return Ok();
            }
            return Unauthorized();

        }

        [HttpPost]
        [Route("ChangeWatchStatus/{movieId}")]
        public IActionResult Watched(string movieId)
        {
            if (string.IsNullOrEmpty(movieId))
            {
                return BadRequest("Movie Id is required");
            }
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                var userMovie = _userMovieService.GetUserMovieByUserIdAndMovieId(userId, movieId);
                if (userMovie == null)
                {
                    return BadRequest();
                }
                _userMovieService.ChangeMovieWatchStatus(userId, movieId, !userMovie.Watched);
                return Ok();
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("ChangeWatchStatus/{movieId}/{status}")]
        public IActionResult Watched(string movieId, bool status)
        {
            if (string.IsNullOrEmpty(movieId))
            {
                return BadRequest("Movie Id is required");
            }
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                _userMovieService.ChangeMovieWatchStatus(userId, movieId, status);
                return Ok();
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("RateMovie/{movieId}/{rating}")]
        public IActionResult RateMovie(string movieId, double rating)
        {
            if (string.IsNullOrEmpty(movieId))
            {
                return BadRequest("Movie Id is required");
            }
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                _userMovieService.RateMovie(userId, movieId, rating);
                return Ok();
            }
            return Unauthorized();
        }



    }
}
