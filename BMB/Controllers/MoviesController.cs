using BMB.Data.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BMB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}
