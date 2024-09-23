using Moq;
using SuppliersManager.Application.Features.Suppliers.Queries;
using SuppliersManager.Application.Interfaces.Services;
using SuppliersManager.Application.Models.Responses.Suppliers;
using SuppliersManager.Shared.Wrapper;

namespace SuppliersManager.Tests.Suppliers.Queries
{
    public class GetSupplierByIdQueryHandlerTests
    {
        private readonly Mock<ISupplierService> _supplierServiceMock;
        private readonly GetSupplierByIdQueryHandler _handler;

        public GetSupplierByIdQueryHandlerTests()
        {
            _supplierServiceMock = new Mock<ISupplierService>();

            _handler = new GetSupplierByIdQueryHandler(_supplierServiceMock.Object);
        }

        [Fact]
        public async Task Handle_Should_GetSupplierById()
        {
            // Arrange
            var query = new GetSupplierByIdQuery("13531351");
            var resultExpected = await Result<SupplierResponse>.SuccessAsync();

            _supplierServiceMock
                .Setup(service => service.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(resultExpected);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            _supplierServiceMock.Verify(repo => repo.GetByIdAsync(It.IsAny<string>()), Times.Once);
            Assert.Equal(resultExpected, result);
        }
    }
}
