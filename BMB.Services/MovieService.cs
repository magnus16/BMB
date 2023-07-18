using BMB.Data.Abstractions;
using BMB.Entities.Models;
using BMB.Services.Abstractions;
using MongoDB.Bson;
using MongoDB.Driver;

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