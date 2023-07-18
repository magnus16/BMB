using BMB.Entities.Models;
using MongoDB.Driver;

namespace BMB.Data.Abstractions
{
    public interface IMongoDBContext
    {
        IMongoClient Client { get; }
        IMongoDatabase Database { get; }
        IMongoCollection<T> GetCollection<T>(string name);
    }
}