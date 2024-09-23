using MediatR;
using SuppliersManager.Application.Interfaces.Services;
using SuppliersManager.Shared.Wrapper;
using System.ComponentModel.DataAnnotations;

namespace SuppliersManager.Application.Features.Users.Commands
{
    public class UpdateUserCommand : IRequest<IResult>
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;

    }

    internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, IResult>
    {
        private readonly IUserService _userService;

        public UpdateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IResult> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            return await _userService.UpdateAsync(command);
        }
    }
}
