using BMB.Entities.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMB.Data.Abstractions
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        //Task CreateAsync(T obj);
        //Task UpdateAsync(string id, T obj);
        //Task DeleteAsync(string id);
        //Task<T> GetByIdAsync(string id);
        //Task<IEnumerable<T>> GetAllAsync();
        //Task<IEnumerable<T>> FindAsync(FilterDefinition<T> filter);

        void Create(T obj);
        void Update(string id, T obj);
        void Delete(string id);
        T GetById(string id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(FilterDefinition<T> filter);

        IMongoCollection<T> GetCollection();
    }
}
