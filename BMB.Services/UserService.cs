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
            return _userRepository.GetById(id);
        }


        public void UpdateUser(User user)
        {
            _userRepository.Update(user.Id, user);
        }
        public bool ValidateUser(string userName, string password)
        {
            bool isValidate = false;
            var userDetails=_userRepository.GetAll().ToList().Where(x=>x.Username==userName && x.Password==password).FirstOrDefault();           
            if (userDetails != null)
            {
                isValidate= true;
            }
            return isValidate;
        }
    }
}