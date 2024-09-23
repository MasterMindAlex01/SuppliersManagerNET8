using MediatR;
using SuppliersManager.Application.Interfaces.Services;
using SuppliersManager.Shared.Wrapper;
using System.ComponentModel.DataAnnotations;

namespace SuppliersManager.Application.Features.Suppliers.Commands
{
    public class UpdateSupplierCommand : IRequest<IResult>
    {
        [Required]
        public string Id { get; set; } = null!;
        [Required]
        public string RegisteredName { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public string City { get; set; } = null!;
        [Required]
        public string State { get; set; } = null!;
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public bool IsActive { get; set; }


    }

    internal class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand, IResult>
    {
        private readonly ISupplierService _supplierService;

        public UpdateSupplierCommandHandler(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        public async Task<IResult> Handle(UpdateSupplierCommand command, CancellationToken cancellationToken)
        {
            return await _supplierService.UpdateAsync(command);
        }
    }
}



