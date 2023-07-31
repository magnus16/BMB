using BMB.API.Controllers;
using BMB.Entities.DTO;
using BMB.Entities.Models;
using BMB.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BMB.API.Test.Controllers
{
    public class MoviesControllerTester
    {
        private readonly Mock<IMovieService> _movieService;
        private const string MOVIE_ID = "abc123";
        private const string MOVIE_TITLE = "title";

        public MoviesControllerTester()
        {
            _movieService = new Mock<IMovieService>();
            _movieService.Setup(m =>
                                m.GetById(It.Is<string>(i => i == MOVIE_ID)))
                         .Returns(GetMovie());
            _movieService.Setup(m =>
                                m.Get(It.Is<MovieSearchParams>(sp =>
                                        !string.IsNullOrEmpty(sp.query) && sp.query == MOVIE_TITLE)))
                        .Returns(GetListOfMovies());
            _movieService.Setup(m =>
                                m.GetAll())
                          .Returns(new List<Movie> { GetMovie() });
        }


        [Theory]
        [InlineData(MOVIE_ID)]
        public void Details_SendId_ReturnsMovieObject(string id)
        {
            var movieController = GetControllerInstance();
            var result = movieController.Details(id);
            var okResult = result as ObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.IsType<Movie>(okResult.Value);
            Assert.Equivalent(GetMovie(), okResult.Value);
        }

        [Theory]
        [InlineData("123abc")]
        public void Details_SendWrongId_ReturnNotFound(string id)
        {
            var movieController = GetControllerInstance();
            var result = movieController.Details(id);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Details_IdIsEmptyOrNull_ThrowsArgumentNullException()
        {
            var movieController = GetControllerInstance();
            Assert.Throws<ArgumentNullException>(() => movieController.Details(movieId: string.Empty));
            Assert.Throws<ArgumentNullException>(() => movieController.Details(movieId: null));
        }


        [Fact]
        public void Search_EmptyMovieSearchParams_ReturnsListOfMatchingMovies()
        {
            var movieController = GetControllerInstance();
            var result = movieController.Search();
            var okResult = result as ObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.IsType<List<Movie>>(okResult.Value);
            Assert.Equivalent(GetListOfMovies(), okResult.Value);
        }

        [Theory]
        [InlineData(MOVIE_TITLE)]
        public void Search_SearchByQuery_ReturnsListOfMatchingMovies(string query)
        {
            MovieSearchParams searchParams = new MovieSearchParams
            {
                query = query
            };
            var movieController = GetControllerInstance();
            var result = movieController.Search(searchParams);
            var okResult = result as ObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.IsType<List<Movie>>(okResult.Value);
            Assert.Equivalent(GetListOfMovies(), okResult.Value);
        }

        [Fact]
        public void Add_InsertMovie_Pass()
        {
            var movieController = GetControllerInstance();
            var result = movieController.Add(GetMovie());
            var okResult = result as ObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void Add_InsertMovieWithNullOrEmptyTitle_ExpectBadRequest()
        {
            var movie = GetMovie();
            movie.Title = string.Empty;
            var movieController = GetControllerInstance();
            var result = movieController.Add(movie);
            var badReqResult = result as ObjectResult;
            Assert.NotNull(badReqResult);
            Assert.Equal("Movie title is required", badReqResult.Value);
            Assert.Equal(StatusCodes.Status400BadRequest, badReqResult.StatusCode);
        }

        [Fact]
        public void Add_InsertNullMovie_ExpectBadRequest()
        {
            Movie movie = null;
            var movieController = GetControllerInstance();
            var result = movieController.Add(movie);
            var badRequestResult = result as BadRequestResult;
            Assert.IsType<BadRequestResult>(badRequestResult);
        }


        [Fact]
        public void Update_UpdateMovie_Pass()
        {
            var movieController = GetControllerInstance();
            var result = movieController.Update(GetMovie());
            var okResult = result as ObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void Update_UpdateMovieWithNullOrEmptyId_ExpectBadRequest()
        {
            var movie = GetMovie();
            movie.Id = string.Empty;
            var movieController = GetControllerInstance();
            var result = movieController.Update(movie);
            var badReqResult = result as ObjectResult;
            Assert.NotNull(badReqResult);
            Assert.Equal("Movie id is required", badReqResult.Value);
            Assert.Equal(StatusCodes.Status400BadRequest, badReqResult.StatusCode);
        }

        [Fact]
        public void Update_UpdateMovieWithNullOrEmptyTitle_ExpectBadRequest()
        {
            var movie = GetMovie();
            movie.Title = string.Empty;
            var movieController = GetControllerInstance();
            var result = movieController.Update(movie);
            var badReqResult = result as ObjectResult;
            Assert.NotNull(badReqResult);
            Assert.Equal("Movie title is required", badReqResult.Value);
            Assert.Equal(StatusCodes.Status400BadRequest, badReqResult.StatusCode);
        }

        [Fact]
        public void Update_UpdateNullMovie_ExpectBadRequest()
        {
            Movie movie = null;
            var movieController = GetControllerInstance();
            var result = movieController.Update(movie);
            var badReqResult = result as BadRequestResult;
            Assert.Equal(StatusCodes.Status400BadRequest, badReqResult.StatusCode);
        }



        [Theory]
        [InlineData(MOVIE_ID)]
        public void Delete_SendMovieId_Pass(string movieId)
        {
            var movieController = GetControllerInstance();
            var result = movieController.Delete(movieId);
            var okResult = result as ObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void Delete_SendNullOrEmptyId_ExpectBadRequest()
        {
            string movieId = string.Empty;
            var movieController = GetControllerInstance();
            var result = movieController.Delete(movieId);
            var badReqResult = result as ObjectResult;
            Assert.NotNull(badReqResult);
            Assert.Equal("Movie id is required", badReqResult.Value);
            Assert.Equal(StatusCodes.Status400BadRequest, badReqResult.StatusCode);
        }




        private MoviesController GetControllerInstance()
        {
            return new MoviesController(_movieService.Object);
        }

        private List<UserMovieDTO> GetListOfMovies()
        {
            return new List<UserMovieDTO>
            {
                new UserMovieDTO()
                {
                    Id = "abc123",
                    ReleaseDate = new DateTime(2023, 01, 01),
                    Title = "Title",
                    Description = "Description",
                    Genre = "Sci-Fi"
                }
        };
        }
        private Movie GetMovie()
        {
            return new Movie()
            {
                Id = "abc123",
                ReleaseDate = new DateTime(2023, 01, 01),
                Title = "Title",
                Description = "Description",
                Genre = "Sci-Fi"
            };
        }
    }
}
