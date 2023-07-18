using BMB.Data.Abstractions;
using BMB.Entities.DTO;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMB.Data
{
    public class MongoDBContext : IMongoDBContext
    {
        public IMongoClient Client { get; }

        public IMongoDatabase Database { get; }

        public MongoDBContext(IOptions<ConnectionSetting> settings)
        {
            Client = new MongoClient(settings.Value.ConnectionString);
            Database = Client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return Database.GetCollection<T>(name);
        }
    }
}
