using BMB.Entities.DTO;
using BMB.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMB.Services.Abstractions
{
    public interface IUserService
    {
        User GetById(string id);
        List<User> GetAll();
        void CreateUser(User user);
        void DeleteUser(string id);
        void UpdateUser(User user);
        bool ValidateUser(string userName, string password, out User? user);

        string HashPassword(string password);
    }
}
