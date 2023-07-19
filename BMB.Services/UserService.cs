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


    }
}