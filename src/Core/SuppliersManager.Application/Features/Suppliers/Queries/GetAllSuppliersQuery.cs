using MediatR;
using SuppliersManager.Application.Interfaces.Services;
using SuppliersManager.Application.Models.Responses.Suppliers;
using SuppliersManager.Shared.Wrapper;

namespace SuppliersManager.Application.Features.Suppliers.Queries
{
    public class GetAllSuppliersQuery : IRequest<PaginatedResult<SupplierResponse>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    internal class GetAllSuppliersQueryHandler : IRequestHandler<GetAllSuppliersQuery, PaginatedResult<SupplierResponse>>
    {
        private readonly ISupplierService _supplierService;

        public GetAllSuppliersQueryHandler(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        public async Task<PaginatedResult<SupplierResponse>> Handle(GetAllSuppliersQuery query, CancellationToken cancellationToken)
        {
            return await _supplierService.GetAllAsync(query.PageNumber, query.PageSize);
        }
    }
}
