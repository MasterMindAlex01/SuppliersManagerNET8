using SuppliersManager.Application.Models.Responses.Suppliers;
using SuppliersManager.Shared.Wrapper;

namespace SuppliersManager.Application.Interfaces.Repositories
{
    public interface ISupplierRepository
    {
        Task<PaginatedResult<SupplierResponse>> GetPagedResponseAsync(int pageNumber, int pageSize);
    }
}
