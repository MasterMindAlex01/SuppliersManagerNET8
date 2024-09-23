using SuppliersManager.Application.Features.Suppliers.Commands;
using SuppliersManager.Application.Models.Responses.Suppliers;
using SuppliersManager.Shared.Wrapper;

namespace SuppliersManager.Application.Interfaces.Services
{
    public interface ISupplierService
    {
        Task<IResult<SupplierResponse>> GetByIdAsync(string id);

        Task<PaginatedResult<SupplierResponse>> GetAllAsync(int pageNumber, int pageSize);

        Task<IResult> AddAsync(CreateSupplierCommand command);

        Task<IResult> UpdateAsync(UpdateSupplierCommand command);

        Task<IResult> DeleteAsync(string id);
    }
}
