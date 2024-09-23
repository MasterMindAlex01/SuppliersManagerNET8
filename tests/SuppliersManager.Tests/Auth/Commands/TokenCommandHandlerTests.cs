using Moq;
using SuppliersManager.Application.Features.Auth.Commands;
using SuppliersManager.Application.Interfaces.Services;
using SuppliersManager.Shared.Wrapper;

namespace SuppliersManager.Tests.Auth.Commands
{
    public class TokenCommandHandlerTests
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly TokenCommandHandler _handler;

        public TokenCommandHandlerTests()
        {
            _authServiceMock = new Mock<IAuthService>();

            _handler = new TokenCommandHandler(_authServiceMock.Object);
        }

        [Fact]
        public async Task Handle_Should_LoginAndResult()
        {
            // Arrange
            var command = new TokenCommand()
            {
                UserName = "Test",
                Password = "Test",
            };
            var expectedId = await Result<TokenResponse>.SuccessAsync();

            _authServiceMock
                .Setup(service => service.LoginJWT(It.IsAny<TokenCommand>()))
                .ReturnsAsync(expectedId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _authServiceMock.Verify(repo => repo.LoginJWT(It.IsAny<TokenCommand>()), Times.Once);
            Assert.Equal(expectedId, result);
        }
    }
}
