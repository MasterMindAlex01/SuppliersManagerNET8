using MediatR;
using SuppliersManager.Application.Interfaces.Services;
using SuppliersManager.Application.Models.Responses.Users;
using SuppliersManager.Shared.Wrapper;

namespace SuppliersManager.Application.Features.Users.Queries
{
    public class GetUserByIdQuery : IRequest<IResult<UserResponse>>
    {
        public GetUserByIdQuery(string id)
        {
            Id = id;
        }
        public string Id { get; set; }
    }

    internal class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, IResult<UserResponse>>
    {
        private readonly IUserService _userService;

        public GetUserByIdQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IResult<UserResponse>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            return await _userService.GetByIdAsync(query.Id);
        }
    }
}
