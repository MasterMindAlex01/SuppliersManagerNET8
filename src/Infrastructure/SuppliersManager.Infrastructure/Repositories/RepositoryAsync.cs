

using MongoDB.Driver;
using SuppliersManager.Application.Interfaces.Repositories;
using SuppliersManager.Domain.Contracts;

namespace SuppliersManager.Infrastructure.MongoDBDriver.Repositories
{
    public class RepositoryAsync<T> : IRepositoryAsync<T> where T : BaseEntity
    {
        private readonly IMongoCollection<T> _collection;

        public RepositoryAsync(IMongoDatabase database)
        {
            string className = typeof(T).Name.ToLower() + "s";
            _collection = database.GetCollection<T>(className);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            await _collection.DeleteOneAsync(x => x.Id == entity.Id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }
    }
}
