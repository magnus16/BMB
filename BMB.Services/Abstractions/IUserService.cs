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
        void AddMovie(string userId, string movieId);
        void RemoveMovie(string userId, string movieId);
        void ChangeMovieStatus(string userId, string movieId, bool watched);
    }
}
