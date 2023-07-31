using BMB.Data.Abstractions;
using BMB.Entities.DTO;
using BMB.Entities.Models;
using BMB.Services.Abstractions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Text.RegularExpressions;

namespace BMB.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IUserMovieRepository _userMovieRepository;
        public MovieService(IMovieRepository movieRepository, IUserMovieRepository userMovieRepository)
        {
            _movieRepository = movieRepository;
            _userMovieRepository = userMovieRepository;
        }

        public void Add(Movie movie)
        {
            _movieRepository.Create(movie);
        }

        public void Delete(string id)
        {
            _movieRepository.Delete(id);
        }
        public List<Movie> GetAll()
        {
            return _movieRepository.GetAll().ToList();
        }
        public List<UserMovieDTO> Get(MovieSearchParams searchParams)
        {

            var _movieCollection = _movieRepository.GetCollection();
            var movieQuery = _movieCollection.AsQueryable();

            FilterDefinition<Movie> filterDef = Builders<Movie>.Filter.Empty;
            if (!string.IsNullOrEmpty(searchParams.query))

            {

                //Method 1
                //this code (commented below) only searches for full words and not partial
                //though it performs much faster because for indexes
                //var filter = Builders<Movie>.Filter.Text(searchParams.searchQuery);//.Regex("Title", new BsonRegularExpression(query));
                //movieQuery = movieQuery.Where(_ => filter.Inject());

                //Method2
                //var regex = new Regex(searchParams.searchQuery, RegexOptions.IgnoreCase);
                //movieQuery = movieQuery.Where(x => regex.IsMatch(x.Title));

                //Method 3 

                filterDef = filterDef & Builders<Movie>.Filter.Regex("Title", new BsonRegularExpression(searchParams.query, "i"));

            }



            if (searchParams.Genre.HasValue)
            {
                var genreText = Enums.GetGenreText(searchParams.Genre.Value);
                //var filter = Builders<Movie>.Filter.Eq("Genre", genreText);
                //movieQuery = movieQuery.Where(_ => filter.Inject());
                filterDef = filterDef & Builders<Movie>.Filter.Eq("Genre", genreText);
            }
            if (searchParams.Year.HasValue)
            {
                DateTime date = new DateTime(searchParams.Year.Value, 1, 1);
                //var filter = Builders<Movie>.Filter.Eq(x => x.ReleaseDate.Value.Year, searchParams.Year.Value);
                //movieQuery = movieQuery.Where(_ => filter.Inject());
                DateTime endDate = date.AddYears(1).AddMinutes(-1);
                filterDef = filterDef & Builders<Movie>.Filter.Gte(x => x.ReleaseDate, date);
                filterDef = filterDef & Builders<Movie>.Filter.Lte(x => x.ReleaseDate, endDate);
            }
            var query = _movieCollection.Find(filterDef);
            if (!string.IsNullOrEmpty(searchParams.sortBy))
            {
                SortDefinition<Movie> sortDef;
                if (searchParams.sort == "asc")
                {
                    sortDef = Builders<Movie>.Sort.Ascending(searchParams.sortBy);
                }
                else
                {
                    sortDef = Builders<Movie>.Sort.Descending(searchParams.sortBy);
                }
                query.Sort(sortDef);
            }



            //return movieQuery.Skip(searchParams.pageSize * (searchParams.pageNumber - 1)).Take(searchParams.pageSize).ToList();

            var movieList = query.Skip(searchParams.pageSize * (searchParams.pageNumber - 1)).Limit(searchParams.pageSize).ToList();
            var movieIds = movieList.Select(m => m.Id).ToList();
            //var userMovies = _userMovieRepository.Find()
            var userMovies = _userMovieRepository.GetCollection().AsQueryable()
                                    .Where(um => movieIds.Any(id => id == um.MovieId))
                                    .ToList();

            var ratedMovies = (from mov in movieList
                               join um in userMovies on mov.Id equals um.MovieId
                               into userMovieJoin
                               from umj in userMovieJoin.DefaultIfEmpty()
                               select new
                               {
                                   Rating = (umj != null) ? umj.Rating : null,
                                   Movie = mov
                               })
                       .GroupBy(g => g.Movie)
                       .Select(g => new UserMovieDTO()
                       {
                           MovieId = g.Key.Id,
                           Description = g.Key.Description,
                           Genre = g.Key.Genre,
                           ImageURL = g.Key.ImageURL,
                           Rating = (g.Where(r => r.Rating.HasValue).Select(r => r.Rating)).Average(),
                           ReleaseDate = g.Key.ReleaseDate,
                           Title = g.Key.Title,
                           UserId = null
                       }).ToList();

            if (!string.IsNullOrEmpty(searchParams.userId))
            {
                var filter = Builders<UserMovie>.Filter.Eq("UserId", ObjectId.Parse(searchParams.userId));
                userMovies = _userMovieRepository.Find(filter).ToList();
                foreach (var um in userMovies)
                {
                    var mov = ratedMovies.Where(m => m.MovieId == um.MovieId).FirstOrDefault();
                    mov.Watched = um.Watched;
                    mov.WatchedOn = um.WatchedOn;
                    mov.UserId = um.UserId;
                }
            }
            return ratedMovies;
        }

        public Movie GetById(string id)
        {
            return _movieRepository.GetById(id);
        }

        public List<Movie> SearchByTitle(string query)
        {
            var filter = Builders<Movie>.Filter.Regex("Title", new BsonRegularExpression(query));
            return _movieRepository.Find(filter).ToList();
        }

        public void Update(Movie movie)
        {
            _movieRepository.Update(movie.Id, movie);
        }
    }
}