using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using SuppliersManager.Application.Interfaces.Repositories;
using SuppliersManager.Domain.Contracts;
using SuppliersManager.Infrastructure.MongoDbEF.Contexts;

namespace SuppliersManager.Infrastructure.MongoDbEF.Repositories
{
    public class MongoEFRepositoryAsync<T> : IRepositoryAsync<T> where T : BaseEntity
    {
        private readonly ApplicationMongoDbContext _dbContext;

        public MongoEFRepositoryAsync(ApplicationMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> Entities => _dbContext.Set<T>();

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext
                .Set<T>()
                .ToListAsync();
        }

        public async Task<T?> GetByIdAsync(ObjectId id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public Task UpdateAsync(T entity)
        {
            T exist = _dbContext.Set<T>().Find(entity.Id)!;
            _dbContext.Entry(exist).CurrentValues.SetValues(entity);
            return Task.CompletedTask;
        }
    }
}
