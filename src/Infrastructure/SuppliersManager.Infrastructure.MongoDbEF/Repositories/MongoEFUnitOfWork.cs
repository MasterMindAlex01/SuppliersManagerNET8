using MongoDB.Driver;
using SuppliersManager.Application.Interfaces.Repositories;
using SuppliersManager.Domain.Contracts;
using System.Collections;

namespace SuppliersManager.Infrastructure.MongoDbEF.Repositories
{
    public class MongoEFUnitOfWork : IUnitOfWork
    {
        private readonly IMongoDatabase _database;
        private bool disposed;
        private Hashtable _repositories = null!;

        public MongoEFUnitOfWork(IMongoDatabase database)
        {
            _database = database;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RollbackTransaction()
        {
            throw new NotImplementedException();
        }

        public IRepositoryAsync<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(MongoEFRepositoryAsync<>);

                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _database);

                _repositories.Add(type, repositoryInstance);
            }

            return (IRepositoryAsync<TEntity>)_repositories[type]!;
        }

        public Task CommitTransaction()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //dispose managed resources
                    //_database.Dispose();
                }
            }
            //dispose unmanaged resources
            disposed = true;
        }
    }
}
