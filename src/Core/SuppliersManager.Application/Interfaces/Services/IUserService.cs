using SuppliersManager.Application.Features.Users.Commands;
using SuppliersManager.Application.Models.Responses.Users;
using SuppliersManager.Shared.Wrapper;

namespace SuppliersManager.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<IResult<UserResponse>> GetByIdAsync(string id);

        Task<PaginatedResult<UserResponse>> GetAllAsync(int pageNumber, int pageSize);

        Task<IResult> AddAsync(CreateUserCommand command);

        Task<IResult> UpdateAsync(UpdateUserCommand command);

        Task<IResult> DeleteAsync(string id);
    }
}
