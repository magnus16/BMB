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
        protected MovieRepository(IMongoDBContext context) : base(context) { }
    }
}
