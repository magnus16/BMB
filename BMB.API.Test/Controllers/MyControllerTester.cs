
using BMB.API.Controllers;
using BMB.Entities.DTO;
using BMB.Entities.Models;
using BMB.Services;
using BMB.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using System.Security.Principal;

namespace BMB.API.Test.Controllers
{
    public class MyControllerTester
    {
        private readonly Mock<IUserMovieService> _userMovieService;

        private ClaimsIdentity _identity;
        private ClaimsPrincipal _user;
        private ControllerContext _controllerContext;

        private const string MOVIE_ID = "abc123";
        private const string MOVIE_TITLE = "title";
        private const string USER_ID = "user_id";
        public MyControllerTester()
        {
            var user = new User { Id = USER_ID };
            var claims = new List<Claim>() { new Claim("Id", USER_ID) };

            _identity = new ClaimsIdentity(claims, "TestUser");
            _user = new ClaimsPrincipal(_identity);
            _controllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = _user } };

            _userMovieService = new Mock<IUserMovieService>();
            _userMovieService.Setup(m =>
                                    m.GetMoviesForUser(It.Is<string>(i => i == USER_ID)))
                             .Returns(GetListOfMovies());
        }

        [Fact]
        public void Get_ExpectListOfUserMovieDTO_Pass()
        {
            var myController = GetControllerInstance();
            var result = myController.Get();
            var okResult = result as ObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.IsType<List<UserMovieDTO>>(okResult.Value);
            Assert.Equivalent(GetListOfMovies(), okResult.Value);
        }

        [Fact]
        public void Get_Unauthoried_ReturnUnauthorized()
        {
            var myController = new MyController(_userMovieService.Object);
            var result = myController.Get();
            var unAuthorizedRes = result as UnauthorizedResult;

            Assert.NotNull(unAuthorizedRes);
            Assert.Equal(StatusCodes.Status401Unauthorized, unAuthorizedRes.StatusCode);
        }

        [Theory]
        [InlineData(MOVIE_ID)]
        public void AddMovie_UseCorrectMovieId_Pass(string movieId)
        {
            var myController = GetControllerInstance();
            var result = myController.AddMovie(movieId);
            var okResult = result as ObjectResult;

            Assert.NotNull (okResult);
            Assert.Equal(StatusCodes.Status200OK,okResult.StatusCode);
        }

        [Fact]
        public void AddMovie_EmptyOrNullMovieId_ReturnBadRequest()
        {
            var myController = GetControllerInstance();
            var result = myController.AddMovie(string.Empty);
            var badRequestResult = result as ObjectResult;

            Assert.NotNull(badRequestResult);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.Equal("Movie Id is required",badRequestResult.Value);
        }

        [Theory]
        [InlineData(MOVIE_ID)]
        public void RemoveMovie_UseCorrectMovieId_Pass(string movieId)
        {
            var myController = GetControllerInstance();
            var result = myController.RemoveMovie(movieId);
            var okResult = result as ObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void RemoveMovie_EmptyOrNullMovieId_ReturnBadRequest()
        {
            var myController = GetControllerInstance();
            var result = myController.RemoveMovie(string.Empty);
            var badRequestResult = result as ObjectResult;

            Assert.NotNull(badRequestResult);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.Equal("Movie Id is required", badRequestResult.Value);
        }


        [Theory]
        [InlineData(MOVIE_ID)]
        public void Watched_UseCorrectMovieId_Pass(string movieId)
        {
            var myController = GetControllerInstance();
            var result = myController.Watched(movieId);
            var okResult = result as ObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void Watched_EmptyOrNullMovieId_ReturnBadRequest()
        {
            var myController = GetControllerInstance();
            var result = myController.Watched(string.Empty);
            var badRequestResult = result as ObjectResult;

            Assert.NotNull(badRequestResult);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.Equal("Movie Id is required", badRequestResult.Value);
        }




        private MyController GetControllerInstance()
        {
            return new MyController(_userMovieService.Object) { ControllerContext = _controllerContext };
        }
        private List<UserMovieDTO> GetListOfMovies()
        {
            return new List<UserMovieDTO> { GetMovie() };
        }
        private UserMovieDTO GetMovie()
        {
            return new UserMovieDTO()
            {
                MovieId = MOVIE_ID,
                Watched = false,
                WatchedOn = null,
                ReleaseDate = new DateTime(2023, 01, 01),
                Title = MOVIE_TITLE,
                Description = "Description",
                Genre = "Sci-Fi",
                Rating = 10
            };
        }
    }
}
