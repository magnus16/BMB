using BMB.Entities.Models;
using BMB.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BMB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IUserService _userService;

        public MyController(IMovieService movieService, IUserService userService)
        {
            _movieService = movieService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            string loggedInUserId = "";

            //User user = 

            return Ok();
        }

    }
}
