using BMB.Data.Abstractions;
using BMB.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMB.Data
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        public MovieRepository(IMongoDBContext context) : base(context) { }
    }

    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IMongoDBContext context) : base(context) { }
    }

    public class UserMovieRepository : BaseRepository<UserMovie>, IUserMovieRepository
    {
        public UserMovieRepository(IMongoDBContext context) : base(context) { }
    }
}
