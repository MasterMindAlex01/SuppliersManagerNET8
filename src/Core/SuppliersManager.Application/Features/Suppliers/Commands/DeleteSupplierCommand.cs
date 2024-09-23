using MediatR;
using SuppliersManager.Application.Interfaces.Services;
using SuppliersManager.Shared.Wrapper;

namespace SuppliersManager.Application.Features.Suppliers.Commands
{
    public class DeleteSupplierCommand : IRequest<IResult>
    {
        public DeleteSupplierCommand(string id)
        {
            Id = id;
        }
        public string Id { get; set; }

    }

    internal class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand, IResult>
    {
        private readonly ISupplierService _supplierService;

        public DeleteSupplierCommandHandler(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        public async Task<IResult> Handle(DeleteSupplierCommand command, CancellationToken cancellationToken)
        {
            return await _supplierService.DeleteAsync(command.Id);
        }
    }
}
