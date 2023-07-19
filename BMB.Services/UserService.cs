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
            throw new NotImplementedException();
        }

        public void CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(string id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void MarkMovieAsWatched(string userId, string movieId)
        {
            throw new NotImplementedException();
        }

        public void RemoveMovie(string userId, string movieId)
        {
            throw new NotImplementedException();
        }
    }
}