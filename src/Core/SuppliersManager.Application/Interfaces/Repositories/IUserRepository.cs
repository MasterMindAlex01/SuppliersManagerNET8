using SuppliersManager.Application.Models.Responses.Users;
using SuppliersManager.Domain.Entities;
using SuppliersManager.Shared.Wrapper;

namespace SuppliersManager.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByUserNameAsync(string username);
        Task<PaginatedResult<UserResponse>> GetPagedResponseAsync(int pageNumber, int pageSize);
    }
}
