using BMB.Data;
using BMB.Data.Abstractions;
using BMB.Entities.Models;
using BMB.Services.Abstractions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BMB.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    
        public void Add(User user)
        {
            _userRepository.Create(user);
        }
       
        public void Delete(string id)
        {
            _userRepository.Delete(id);
        }

        public List<User> GetAll()
        {
            return _userRepository.GetAll().ToList();
        }

        public User GetById(string id)
        {
            return _userRepository.GetById(id);
        }

    }
}