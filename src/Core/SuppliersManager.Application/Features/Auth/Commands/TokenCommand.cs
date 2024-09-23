using MediatR;
using SuppliersManager.Application.Interfaces.Services;
using SuppliersManager.Shared.Wrapper;

namespace SuppliersManager.Application.Features.Auth.Commands
{
    public class TokenCommand : IRequest<IResult<TokenResponse>>
    {
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;

    }

    internal class TokenCommandHandler : IRequestHandler<TokenCommand, IResult<TokenResponse>>
    {
        private readonly IAuthService _authService;

        public TokenCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<IResult<TokenResponse>> Handle(TokenCommand command, CancellationToken cancellationToken)
        {
           return await _authService.LoginJWT(command);
        }
    }

    public class TokenResponse
    {
        public string AccessToken { get; set; } = default!;
    }
}
