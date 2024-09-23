using Moq;
using SuppliersManager.Application.Features.Users.Commands;
using SuppliersManager.Application.Interfaces.Services;
using SuppliersManager.Shared.Wrapper;

namespace SuppliersManager.Tests.Users.Commands
{
    public class CreateUserCommandHandlerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly CreateUserCommandHandler _handler;

        public CreateUserCommandHandlerTests()
        {
            _userServiceMock = new Mock<IUserService>();

            _handler = new CreateUserCommandHandler(_userServiceMock.Object);
        }

        [Fact]
        public async Task Handle_Should_CreateUserAndReturnId()
        {
            // Arrange
            var command = new CreateUserCommand()
            {
                Email = "alex@example.com",
                UserName = "Test",
                FirstName = "Test",
                LastName = "Test",
                Password = "123456",
                ConfirmPassword = "123456"
            };
            var expectedId = await Result<string>.SuccessAsync("66e9e46ffd4b3a74c70995b3", "");

            _userServiceMock
                .Setup(service => service.AddAsync(It.IsAny<CreateUserCommand>()))
                .ReturnsAsync(expectedId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _userServiceMock.Verify(repo => repo.AddAsync(It.IsAny<CreateUserCommand>()), Times.Once);
            Assert.Equal(expectedId, result);
        }
    }
}
