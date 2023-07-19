using BMB.Data;
using BMB.Data.Abstractions;
using BMB.Entities.Models;
using BMB.Services.Abstractions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BMB.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void AddMovie(string userId, string movieId)
        {
            var user = _userRepository.GetById(userId);
            if (user.Movies == null)
            {
                user.Movies = new List<UserMovie>();
            }
            user.Movies.Add(new UserMovie()
            {
                MovieId = movieId
            });
            UpdateUser(user);
        }

        public void CreateUser(User user)
        {
            _userRepository.Create(user);
        }

        public void DeleteUser(string id)
        {
            _userRepository.Delete(id);
        }


        public List<User> GetAll()
        {
            return _userRepository.GetAll().ToList();
        }

        public User GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void MarkMovieAsWatched(string userId, string movieId)
        {
            throw new NotImplementedException();
        }

        public void ChangeMovieStatus(string userId, string movieId, bool watched)
        {
            User user = GetById(userId);
            if (user.Movies == null)
            {
                throw new Exception("Movie doesn't exist in user's list.");
            }
            var movie = user.Movies.Where(m => m.MovieId == movieId).FirstOrDefault();
            if (movie != null)
            {
                movie.Watched = watched;
                movie.WatchedOn = watched ? DateTime.UtcNow : null;
            }
            UpdateUser(user);
        }

        public void RemoveMovie(string userId, string movieId)
        {
            User user = GetById(userId);
            if (user.Movies == null)
            {
                throw new Exception("Movie doesn't exist in user's list.");
            }

            var movie = user.Movies.Where(m => m.MovieId == movieId).FirstOrDefault();
            if (movie != null)
            {
                user.Movies.Remove(movie);
            }
            UpdateUser(user);
        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user.Id, user);
        }
    }
}