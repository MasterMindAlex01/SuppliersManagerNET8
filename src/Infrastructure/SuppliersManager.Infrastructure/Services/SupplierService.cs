using SuppliersManager.Application.Features.Suppliers.Commands;
using SuppliersManager.Application.Interfaces.Repositories;
using SuppliersManager.Application.Interfaces.Services;
using SuppliersManager.Application.Models.Responses.Suppliers;
using SuppliersManager.Domain.Entities;
using SuppliersManager.Shared.Wrapper;

namespace SuppliersManager.Infrastructure.MongoDBDriver.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(
            IUnitOfWork unitOfWork, 
            ICurrentUserService currentUserService,
            ISupplierRepository supplierRepository)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _supplierRepository = supplierRepository;
        }

        public async Task<IResult> AddAsync(CreateSupplierCommand command)
        {
            var supplier = new Supplier
            {
                IsActive = true,
                RegisteredName = command.RegisteredName,
                Address = command.Address,
                City = command.City,
                State = command.State,
                Email = command.Email,
                TIN = command.TIN,
                CreateByContact = _currentUserService.GetUserId()!,
                CreationDate = DateTime.UtcNow,
                EmailContact = _currentUserService.GetUserEmail()!,
            };
            
            await _unitOfWork.Repository<Supplier>().AddAsync(supplier);

            return await Result<string>.SuccessAsync(supplier.Id, "Supplier created");
        }

        public async Task<IResult> DeleteAsync(string id)
        {
            var currentSupplier = await _unitOfWork.Repository<Supplier>().GetByIdAsync(id);
            if (currentSupplier == null) return await Result.FailAsync("Supplier not found");

            await _unitOfWork.Repository<Supplier>().DeleteAsync(currentSupplier);

            return await Result.SuccessAsync("Supplier deleted");
        }

        public async Task<PaginatedResult<SupplierResponse>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _supplierRepository.GetPagedResponseAsync(pageNumber, pageSize);
        }

        public async Task<IResult<SupplierResponse>> GetByIdAsync(string id)
        {
            var currentSupplier = await _unitOfWork.Repository<Supplier>().GetByIdAsync(id);
            if (currentSupplier == null) return await Result<SupplierResponse>.FailAsync("Supplier not found");
            var userResponse = new SupplierResponse()
            {
                Id = currentSupplier.Id,
                Email = currentSupplier.Email,
                RegisteredName = currentSupplier.RegisteredName,
                State = currentSupplier.State,
                Address = currentSupplier.Address,
                City = currentSupplier.City,
                IsActive = currentSupplier.IsActive,
                TIN = currentSupplier.TIN,
            };
            return await Result<SupplierResponse>.SuccessAsync(userResponse, "Ok");
        }

        public async Task<IResult> UpdateAsync(UpdateSupplierCommand command)
        {
            var currentSupplier = await _unitOfWork.Repository<Supplier>().GetByIdAsync(command.Id);
            if (currentSupplier == null) return await Result.FailAsync("Supplier not found");

            currentSupplier.Email = command.Email;
            currentSupplier.RegisteredName = command.RegisteredName;
            currentSupplier.Address = command.Address;
            currentSupplier.City = command.City;
            currentSupplier.State = command.State;
            currentSupplier.IsActive = command.IsActive;

            await _unitOfWork.Repository<Supplier>().UpdateAsync(currentSupplier);

            return await Result.SuccessAsync("Supplier updated");
        }
    }
}
