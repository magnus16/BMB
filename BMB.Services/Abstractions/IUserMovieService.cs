using BMB.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMB.Services.Abstractions
{
    public interface IUserMovieService
    {
        void AddMovieToUserList(string userId, string movieId);
        void RemoveMovieFromUserList(string userId, string movieId);
        void Delete(string id);
        void ChangeMovieWatchStatus(string userId, string movieId, bool watched);
        List<UserMovieDTO> GetMoviesForUser(string userId);
    }
}
