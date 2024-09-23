using Microsoft.Extensions.DependencyInjection;
using SuppliersManager.Application.Interfaces.Repositories;
using SuppliersManager.Infrastructure.MongoDbEF.Repositories;

namespace SuppliersManager.Infrastructure.MongoDbEF.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped(typeof(IRepositoryAsync<>), typeof(MongoEFRepositoryAsync<>))
                .AddScoped<IUserRepository, MongoEFUserRepository>()
                .AddScoped<ISupplierRepository, MongoEFSupplierRepository>()
                .AddScoped(typeof(IUnitOfWork), typeof(MongoEFUnitOfWork));
        }
    }
}
