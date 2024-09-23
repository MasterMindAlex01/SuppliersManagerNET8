using Microsoft.EntityFrameworkCore;
using SuppliersManager.Application.Extensions;
using SuppliersManager.Application.Interfaces.Repositories;
using SuppliersManager.Application.Models.Responses.Suppliers;
using SuppliersManager.Infrastructure.MongoDbEF.Contexts;
using SuppliersManager.Shared.Wrapper;

namespace SuppliersManager.Infrastructure.MongoDbEF.Repositories
{
    public class MongoEFSupplierRepository : ISupplierRepository
    {
        private readonly ApplicationMongoDbContext _dbContext;

        public MongoEFSupplierRepository(ApplicationMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<PaginatedResult<SupplierResponse>> GetPagedResponseAsync(int pageNumber, int pageSize)
        {
            return await _dbContext.Suppliers.Select(x => new SupplierResponse
            {
                Id = x.Id.ToString(),
                Email = x.Email,
                Address = x.Address,
                City = x.City,
                IsActive = x.IsActive,
                TIN = x.TIN,
                State = x.State,
                RegisteredName = x.RegisteredName,
            }).AsNoTracking().ToPaginatedListAsync(pageNumber, pageSize);

        }
    }
}
