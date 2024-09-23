using Moq;
using SuppliersManager.Application.Features.Suppliers.Commands;
using SuppliersManager.Application.Interfaces.Services;
using SuppliersManager.Shared.Wrapper;

namespace SuppliersManager.Tests.Suppliers.Commands
{
    public class DeleteSupplierCommandHandlerTests
    {
        private readonly Mock<ISupplierService> _supplierServiceMock;
        private readonly DeleteSupplierCommandHandler _handler;

        public DeleteSupplierCommandHandlerTests()
        {
            _supplierServiceMock = new Mock<ISupplierService>();

            _handler = new DeleteSupplierCommandHandler(_supplierServiceMock.Object);
        }

        [Fact]
        public async Task Handle_Should_DeleteSupplierAndReturnResult()
        {
            // Arrange
            string id = Guid.NewGuid().ToString();
            var command = new DeleteSupplierCommand(id);
            var resultExpected = await Result.SuccessAsync();

            _supplierServiceMock
                .Setup(service => service.DeleteAsync(It.IsAny<string>()))
                .ReturnsAsync(resultExpected);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _supplierServiceMock.Verify(repo => repo.DeleteAsync(It.IsAny<string>()), Times.Once);
            Assert.Equal(resultExpected, result);
        }
    }
}
