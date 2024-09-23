
using SuppliersManager.Domain.Contracts;
using System.Security.Cryptography;

namespace SuppliersManager.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositoryAsync<T> Repository<T>() where T : BaseEntity;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        Task CommitTransaction();

        Task RollbackTransaction();
    }
}
