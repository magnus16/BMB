using BMB.Data;
using BMB.Data.Abstractions;
using BMB.Entities.Models;
using BMB.Services.Abstractions;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Cryptography;
using System.Text;

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
            user.Password=HashPassword(user.Password);
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
        public bool ValidateUser(string userName, string password, out User? user)
        {
            var pwd = HashPassword(password);
            var filter = Builders<User>.Filter.Where(usr =>usr.Username == userName && usr.Password == pwd);
            user = _userRepository.Find(filter).FirstOrDefault();
            return user != null;
        }
        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
               var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                // Get the hashed string.  
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();               
                return hash;
            }
        }
    }
}