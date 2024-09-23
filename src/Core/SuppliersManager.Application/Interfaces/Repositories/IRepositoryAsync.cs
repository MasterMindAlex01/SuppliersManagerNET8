using MongoDB.Bson;
using SuppliersManager.Domain.Contracts;

namespace SuppliersManager.Application.Interfaces.Repositories
{
    public interface IRepositoryAsync<T> where T : BaseEntity
    {
        IQueryable<T> Entities { get; }
        Task<T?> GetByIdAsync(ObjectId id);

        Task<List<T>> GetAllAsync();

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}
