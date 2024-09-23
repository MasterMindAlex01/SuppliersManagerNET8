using MongoDB.Driver;
using SuppliersManager.Application.Interfaces.Repositories;
using SuppliersManager.Domain.Contracts;
using SuppliersManager.Infrastructure.MongoDbEF.Contexts;
using System.Collections;

namespace SuppliersManager.Infrastructure.MongoDbEF.Repositories
{
    public class MongoEFUnitOfWork : IUnitOfWork
    {
        private readonly ApplicationMongoDbContext _dbContext;
        private bool disposed;
        private Hashtable _repositories = null!;

        public MongoEFUnitOfWork(ApplicationMongoDbContext dbContext)
        {
           _dbContext = dbContext;
        }

        public Task DetectChanges()
        {
            _dbContext.ChangeTracker.DetectChanges();
            Console.WriteLine(_dbContext.ChangeTracker.DebugView.LongView);
            return Task.CompletedTask;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
           return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task RollbackTransaction()
        {
            await _dbContext.Database.RollbackTransactionAsync();
        }

        public IRepositoryAsync<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(MongoEFRepositoryAsync<>);

                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dbContext);

                _repositories.Add(type, repositoryInstance);
            }

            return (IRepositoryAsync<TEntity>)_repositories[type]!;
        }

        public async Task CommitTransaction()
        {
            await _dbContext.Database.CommitTransactionAsync();
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
                    _dbContext.Dispose();
                }
            }
            //dispose unmanaged resources
            disposed = true;
        }
    }
}
