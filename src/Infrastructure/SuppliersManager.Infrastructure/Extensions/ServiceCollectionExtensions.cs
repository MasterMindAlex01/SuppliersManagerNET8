using Microsoft.Extensions.DependencyInjection;
using SuppliersManager.Application.Interfaces.Repositories;
using SuppliersManager.Infrastructure.MongoDBDriver.Repositories;

namespace SuppliersManager.Infrastructure.MongoDBDriver.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>))
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<ISupplierRepository, SupplierRepository>()
                .AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        }
    }
}
