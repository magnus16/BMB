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
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
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
        public List<Movie> Get(MovieSearchParams searchParams)
        {
            if (searchParams == null)
            {
                return GetAll();
            }
            var _collection = _movieRepository.GetCollection();
            var movieQuery = _collection.AsQueryable();
            FilterDefinition<Movie> filterDef = Builders<Movie>.Filter.Empty;
            if (!string.IsNullOrEmpty(searchParams.searchQuery))
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
                filterDef = filterDef & Builders<Movie>.Filter.Regex("Title", new BsonRegularExpression(searchParams.searchQuery));

            }



            if (searchParams.GenreType.HasValue)
            {
                var genreText = Enums.GetGenreText(searchParams.GenreType.Value);
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
            var query = _collection.Find(filterDef);
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
            return query.Skip(searchParams.pageSize * (searchParams.pageNumber - 1)).Limit(searchParams.pageSize).ToList();
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