using Moq;
using SuppliersManager.Application.Features.Suppliers.Commands;
using SuppliersManager.Application.Interfaces.Services;
using SuppliersManager.Shared.Wrapper;

namespace SuppliersManager.Tests.Suppliers.Commands
{
    public class UpdateSupplierCommandHandlerTests
    {
        private readonly Mock<ISupplierService> _supplierServiceMock;
        private readonly UpdateSupplierCommandHandler _handler;

        public UpdateSupplierCommandHandlerTests()
        {
            _supplierServiceMock = new Mock<ISupplierService>();

            _handler = new UpdateSupplierCommandHandler(_supplierServiceMock.Object);
        }

        [Fact]
        public async Task Handle_Should_UpdateSupplierAndReturnResult()
        {
            // Arrange
            var command = new UpdateSupplierCommand()
            {
                Id = "66e9e46ffd4b3a74c70995b3",
                Address = "Cra 50 # 40 -45",
                City = "Medellin",
                RegisteredName = "Empresa ZZZ SAS",
                Email = "empresaz@example.com",
                State = "state"
            };
            var resultExpected = await Result.SuccessAsync();

            _supplierServiceMock
                .Setup(service => service.UpdateAsync(It.IsAny<UpdateSupplierCommand>()))
                .ReturnsAsync(resultExpected);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _supplierServiceMock.Verify(repo => repo.UpdateAsync(It.IsAny<UpdateSupplierCommand>()), Times.Once);
            Assert.Equal(resultExpected, result);
        }
    }
}
