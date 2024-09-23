using MediatR;
using SuppliersManager.Application.Interfaces.Services;
using SuppliersManager.Shared.Wrapper;

namespace SuppliersManager.Application.Features.Suppliers.Queries
{
    public class GetSupplierByIdQuery : IRequest<IResult>
    {
        public GetSupplierByIdQuery(string id)
        {
            Id = id;
        }
        public string Id { get; set; }
    }

    internal class GetSupplierByIdQueryHandler : IRequestHandler<GetSupplierByIdQuery, IResult>
    {
        private readonly ISupplierService _supplierService;

        public GetSupplierByIdQueryHandler(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        public async Task<IResult> Handle(GetSupplierByIdQuery query, CancellationToken cancellationToken)
        {
            return await _supplierService.GetByIdAsync(query.Id);
        }
    }
}
