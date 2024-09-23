using MediatR;
using SuppliersManager.Application.Interfaces.Services;
using SuppliersManager.Shared.Wrapper;
using System.ComponentModel.DataAnnotations;

namespace SuppliersManager.Application.Features.Users.Commands
{
    public class CreateUserCommand : IRequest<IResult>
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required, Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = null!;

    }

    internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IResult>
    {
        private readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            return await _userService.AddAsync(command);
        }
    }
}
