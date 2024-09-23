using MediatR;
using SuppliersManager.Application.Interfaces.Services;
using SuppliersManager.Shared.Wrapper;
using System.ComponentModel.DataAnnotations;

namespace SuppliersManager.Application.Features.Suppliers.Commands
{
    public class CreateSupplierCommand : IRequest<IResult>
    {
        [Required]
        public string TIN { get; set; } = null!;
        [Required]
        public string RegisteredName { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public string City { get; set; } = null!;
        [Required]
        public string State { get; set; } = null!;
        [Required,EmailAddress]
        public string Email { get; set; } = null!;

    }

    internal class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, IResult>
    {
        private readonly ISupplierService _supplierService;

        public CreateSupplierCommandHandler(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        public async Task<IResult> Handle(CreateSupplierCommand command, CancellationToken cancellationToken)
        {
            return await _supplierService.AddAsync(command);
        }
    }
}
