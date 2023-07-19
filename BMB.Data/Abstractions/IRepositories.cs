using BMB.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMB.Data.Abstractions
{
    public interface IMovieRepository : IBaseRepository<Movie> { }
    public interface IUserRepository : IBaseRepository<User> { }



}
