using MediatR;
using SuppliersManager.Application.Interfaces.Services;
using SuppliersManager.Application.Models.Responses.Users;
using SuppliersManager.Shared.Wrapper;

namespace SuppliersManager.Application.Features.Users.Queries
{
    public class GetAllUsersQuery : IRequest<PaginatedResult<UserResponse>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    internal class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, PaginatedResult<UserResponse>>
    {
        private readonly IUserService _userService;

        public GetAllUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<PaginatedResult<UserResponse>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            return await _userService.GetAllAsync(query.PageNumber, query.PageSize);
        }
    }
}
