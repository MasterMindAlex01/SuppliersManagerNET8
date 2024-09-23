using Moq;
using SuppliersManager.Application.Features.Users.Queries;
using SuppliersManager.Application.Interfaces.Services;
using SuppliersManager.Application.Models.Responses.Users;
using SuppliersManager.Shared.Wrapper;

namespace SuppliersManager.Tests.Users.Queries
{
    public class GetAllUsersQueryHandlerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly GetAllUsersQueryHandler _handler;

        public GetAllUsersQueryHandlerTests()
        {
            _userServiceMock = new Mock<IUserService>();

            _handler = new GetAllUsersQueryHandler(_userServiceMock.Object);
        }

        [Fact]
        public async Task Handle_Should_GetAllSupplierAndPaged()
        {
            // Arrange
            var list = new List<UserResponse>()
            {
                new UserResponse()
                {
                    Id = "1",
                    UserName = "Test",
                },
                new UserResponse()
                {
                    Id = "2",
                    UserName = "Test2",
                },
            };
            var query = new GetAllUsersQuery()
            {
                PageNumber = 1,
                PageSize = 10,
            };
            var resultExpected = PaginatedResult<UserResponse>.Success(list, 2, 1, 10);

            _userServiceMock
                .Setup(service => service.GetAllAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(resultExpected);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            _userServiceMock.Verify(repo => repo.GetAllAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            Assert.Equal(resultExpected, result);
        }
    }
}
