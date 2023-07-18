using BMB.Data.Abstractions;
using BMB.Entities.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMB.Data
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly IMongoDBContext _context;
        private IMongoCollection<T> _collection;

        protected BaseRepository(IMongoDBContext context)
        {
            _context = context;
            _collection = _context.GetCollection<T>(typeof(T).Name);
        }

        public async Task CreateAsync(T obj)
        {
            if (!string.IsNullOrEmpty(obj.Id))
            {
                obj.Id = string.Empty;
            }
            await _collection.InsertOneAsync(obj);
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            await _collection.DeleteOneAsync(filter);
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            return await _collection.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var list = await _collection.FindAsync(Builders<T>.Filter.Empty);
            return await list.ToListAsync();
        }

        public async Task UpdateAsync(string id, T obj)
        {
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            await _collection.ReplaceOneAsync(filter, obj);
        }

        public async Task<IEnumerable<T>> FindAsync(FilterDefinition<T> filter)
        {
            var res = await _collection.FindAsync(filter);
            return await res.ToListAsync();
        }

        public void Create(T obj)
        {
            if (!string.IsNullOrEmpty(obj.Id))
            {
                obj.Id = string.Empty;
            }
            _collection.InsertOne(obj);
        }

        public void Update(string id, T obj)
        {
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            _collection.ReplaceOne(filter, obj);
        }

        public void Delete(string id)
        {
            _collection.DeleteOne(id);
        }

        public T GetById(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            return _collection.Find(_ => true).ToList();
        }

        public IEnumerable<T> Find(FilterDefinition<T> filter)
        {
            return _collection.Find(filter).ToList();
        }
    }
}
