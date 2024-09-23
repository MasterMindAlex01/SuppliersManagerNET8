using Moq;
using SuppliersManager.Application.Features.Suppliers.Commands;
using SuppliersManager.Application.Features.Users.Commands;
using SuppliersManager.Application.Interfaces.Services;
using SuppliersManager.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuppliersManager.Tests.Users.Commands
{
    public class DeleteUserCommandHandlerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly DeleteUserCommandHandler _handler;

        public DeleteUserCommandHandlerTests()
        {
            _userServiceMock = new Mock<IUserService>();

            _handler = new DeleteUserCommandHandler(_userServiceMock.Object);
        }

        [Fact]
        public async Task Handle_Should_DeleteUserAndReturnResult()
        {
            // Arrange
            string id = Guid.NewGuid().ToString();
            var command = new DeleteUserCommand(id);
            var resultExpected = await Result.SuccessAsync();

            _userServiceMock
                .Setup(service => service.DeleteAsync(It.IsAny<string>()))
                .ReturnsAsync(resultExpected);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _userServiceMock.Verify(repo => repo.DeleteAsync(It.IsAny<string>()), Times.Once);
            Assert.Equal(resultExpected, result);
        }
    }
}
