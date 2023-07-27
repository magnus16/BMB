using BMB.Data;
using BMB.Data.Abstractions;
using BMB.Entities.DTO;
using BMB.Entities.Models;
using BMB.Services.Abstractions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BMB.Services
{
    public class UserMovieService : IUserMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IUserMovieRepository _userMovieRepository;
        private readonly IUserRepository _userRepository;

        public UserMovieService(IMovieRepository movieRepository,
            IUserMovieRepository userMovieRepository)
        {
            _movieRepository = movieRepository;
            _userMovieRepository = userMovieRepository;
        }
        public UserMovieService(IMovieRepository movieRepository,
            IUserMovieRepository userMovieRepository,
            IUserRepository userRepository)
        {
            _movieRepository = movieRepository;
            _userMovieRepository = userMovieRepository;
            _userRepository = userRepository;
        }

        public void AddMovieToUserList(string userId, string movieId)
        {
            var filter = Builders<UserMovie>.Filter.Where(um => um.UserId == userId && um.MovieId == movieId);
            var userMovie = _userMovieRepository.Find(filter).FirstOrDefault();
            if (userMovie == null)
            {
                _userMovieRepository.Create(new UserMovie
                {
                    UserId = userId,
                    MovieId = movieId
                });
            }
        }


        public void ChangeMovieWatchStatus(string userId, string movieId, bool watched)
        {
            var filter = Builders<UserMovie>.Filter.Where(um => um.UserId == userId && um.MovieId == movieId);
            var userMovie = _userMovieRepository.Find(filter).FirstOrDefault();
            if (userMovie != null)
            {
                userMovie.Watched = watched;
                userMovie.WatchedOn = watched ? DateTime.UtcNow : null;
                _userMovieRepository.Update(userMovie.Id, userMovie);
            }
        }

        public void Delete(string id)
        {
            _userMovieRepository.Delete(id);
        }

        public List<UserMovieDTO> GetMoviesForUser(string userId)
        {

            var movieCollection = _movieRepository.GetCollection();
            var userMovieCollection = _userMovieRepository.GetCollection();
            var userCollectiomn = _userRepository.GetCollection();


            List<UserMovieDTO> userMoviesDTO = userMovieCollection.AsQueryable()
                                    .Where(u => u.UserId == userId)
                                    .Join(movieCollection.AsQueryable(), um => um.MovieId, mov => mov.Id, (um, mov) => new UserMovieDTO()
                                    {
                                        MovieId = um.MovieId,
                                        Watched = um.Watched,
                                        WatchedOn = um.WatchedOn,
                                        Description = mov.Description,
                                        Genre = mov.Genre,
                                        ImageURL = mov.ImageURL,
                                        Rating = mov.Rating,
                                        ReleaseDate = mov.ReleaseDate,
                                        Title = mov.Title,
                                        UserId = um.UserId
                                    }).ToList();
            return userMoviesDTO;
        }

        public UserMovie GetUserMovieByUserIdAndMovieId(string userId, string movieId)
        {
            var filter = Builders<UserMovie>.Filter.Where(um => um.UserId == userId && um.MovieId == movieId);
            return _userMovieRepository.Find(filter).FirstOrDefault();
        }

        public void RemoveMovieFromUserList(string userId, string movieId)
        {
            var filter = Builders<UserMovie>.Filter.Where(um => um.UserId == userId && um.MovieId == movieId);
            var userMovie = _userMovieRepository.Find(filter).FirstOrDefault();
            if (userMovie == null)
            {
                throw new Exception("Movie doesn't exist in user's list.");
            }

            _userMovieRepository.Delete(userMovie.Id);

        }

    }
}