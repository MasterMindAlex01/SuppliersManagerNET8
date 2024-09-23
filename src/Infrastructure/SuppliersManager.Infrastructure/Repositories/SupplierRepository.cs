using MongoDB.Driver;
using SuppliersManager.Application.Extensions;
using SuppliersManager.Application.Interfaces.Repositories;
using SuppliersManager.Application.Models.Responses.Suppliers;
using SuppliersManager.Domain.Entities;
using SuppliersManager.Shared.Wrapper;

namespace SuppliersManager.Infrastructure.MongoDBDriver.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly IMongoCollection<Supplier> _collection;

        public SupplierRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Supplier>("suppliers");
        }
        public async Task<PaginatedResult<SupplierResponse>> GetPagedResponseAsync(int pageNumber, int pageSize)
        {
            var paged = await _collection.Find(_ => true).ToPaginatedListAsync(pageNumber, pageSize);

            var list = paged.Data.Select(x => new SupplierResponse
            {
                Id = x.Id,
                Email = x.Email,
                Address = x.Address,
                City = x.City,
                IsActive = x.IsActive,
                TIN = x.TIN,
                State = x.State,
                RegisteredName = x.RegisteredName,
            }).ToList();

            return new PaginatedResult<SupplierResponse>(list)
            {
                CurrentPage = paged.CurrentPage,
                PageSize = paged.PageSize,
                Messages = paged.Messages,
                Succeeded = paged.Succeeded,
                TotalCount = paged.TotalCount,
                TotalPages = paged.TotalPages,

            };
        }
    }
}
