using Moq;
using SuppliersManager.Application.Features.Suppliers.Queries;
using SuppliersManager.Application.Interfaces.Services;
using SuppliersManager.Application.Models.Responses.Suppliers;
using SuppliersManager.Shared.Wrapper;

namespace SuppliersManager.Tests.Suppliers.Queries
{
    public class GetAllSuppliersQueryHandlerTests
    {
        private readonly Mock<ISupplierService> _supplierServiceMock;
        private readonly GetAllSuppliersQueryHandler _handler;

        public GetAllSuppliersQueryHandlerTests()
        {
            _supplierServiceMock = new Mock<ISupplierService>();

            _handler = new GetAllSuppliersQueryHandler(_supplierServiceMock.Object);
        }

        [Fact]
        public async Task Handle_Should_GetAllSupplierAndPaged()
        {
            // Arrange
            var list = new List<SupplierResponse>()
            {
                new SupplierResponse()
                {
                    Id = "1",
                    RegisteredName = "Test",
                },                
                new SupplierResponse()
                {
                    Id = "2",
                    RegisteredName = "Test2",
                },
            };
            var query =  new GetAllSuppliersQuery()
            {
                PageNumber = 1,
                PageSize = 10,
            };
            var resultExpected = PaginatedResult<SupplierResponse>.Success(list, 2, 1, 10);

            _supplierServiceMock
                .Setup(service => service.GetAllAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(resultExpected);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            _supplierServiceMock.Verify(repo => repo.GetAllAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            Assert.Equal(resultExpected, result);
        }
    }
}
