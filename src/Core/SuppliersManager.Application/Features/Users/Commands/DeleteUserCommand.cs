using MediatR;
using SuppliersManager.Application.Interfaces.Services;
using SuppliersManager.Shared.Wrapper;
using System.ComponentModel.DataAnnotations;

namespace SuppliersManager.Application.Features.Users.Commands
{
    public class DeleteUserCommand : IRequest<IResult>
    {
        public DeleteUserCommand(string id)
        {
            Id = id;
        }

        [Required]
        public string Id { get; set; } = null!;

    }

    internal class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, IResult>
    {
        private readonly IUserService _userService;

        public DeleteUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IResult> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            return await _userService.DeleteAsync(command.Id);
        }
    }
}
