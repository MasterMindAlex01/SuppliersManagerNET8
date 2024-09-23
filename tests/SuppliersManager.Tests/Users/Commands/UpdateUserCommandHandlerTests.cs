using Moq;
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
    public class UpdateUserCommandHandlerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly UpdateUserCommandHandler _handler;

        public UpdateUserCommandHandlerTests()
        {
            _userServiceMock = new Mock<IUserService>();

            _handler = new UpdateUserCommandHandler(_userServiceMock.Object);
        }

        [Fact]
        public async Task Handle_Should_UpdateUserAndReturnResult()
        {
            // Arrange
            var command = new UpdateUserCommand()
            {
                Id = Guid.NewGuid().ToString(),
                LastName = "Test",
                FirstName = "Test",
                Email = "Test",
            };
            var resultExpected = await Result.SuccessAsync();

            _userServiceMock
                .Setup(service => service.UpdateAsync(It.IsAny<UpdateUserCommand>()))
                .ReturnsAsync(resultExpected);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _userServiceMock.Verify(repo => repo.UpdateAsync(It.IsAny<UpdateUserCommand>()), Times.Once);
            Assert.Equal(resultExpected, result);
        }
    }
}
