using Moq;
using SuppliersManager.Application.Features.Users.Queries;
using SuppliersManager.Application.Interfaces.Services;
using SuppliersManager.Application.Models.Responses.Users;
using SuppliersManager.Shared.Wrapper;

namespace SuppliersManager.Tests.Users.Queries
{
    public class GetUserByIdQueryHandlerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly GetUserByIdQueryHandler _handler;

        public GetUserByIdQueryHandlerTests()
        {
            _userServiceMock = new Mock<IUserService>();

            _handler = new GetUserByIdQueryHandler(_userServiceMock.Object);
        }

        [Fact]
        public async Task Handle_Should_GetSupplierById()
        {
            // Arrange
            var query = new GetUserByIdQuery("13531351");
            var resultExpected = await Result<UserResponse>.SuccessAsync();

            _userServiceMock
                .Setup(service => service.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(resultExpected);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            _userServiceMock.Verify(repo => repo.GetByIdAsync(It.IsAny<string>()), Times.Once);
            Assert.Equal(resultExpected, result);
        }
    }
}
