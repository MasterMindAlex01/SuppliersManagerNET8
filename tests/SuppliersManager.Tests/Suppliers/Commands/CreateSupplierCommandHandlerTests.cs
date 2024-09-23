using Moq;
using SuppliersManager.Application.Features.Suppliers.Commands;
using SuppliersManager.Application.Interfaces.Services;
using SuppliersManager.Shared.Wrapper;

namespace SuppliersManager.Tests.Suppliers.Commands
{
    public class CreateSupplierCommandHandlerTests
    {
        private readonly Mock<ISupplierService> _supplierServiceMock;
        private readonly CreateSupplierCommandHandler _handler;

        public CreateSupplierCommandHandlerTests()
        {
            _supplierServiceMock = new Mock<ISupplierService>();

            _handler = new CreateSupplierCommandHandler(_supplierServiceMock.Object);
        }

        [Fact]
        public async Task Handle_Should_CreateSupplierAndReturnId()
        {
            // Arrange
            var command = new CreateSupplierCommand()
            {
                Address = "Cra 50 # 40 -45",
                City = "Medellin",
                RegisteredName = "Empresa ZZZ SAS",
                Email = "empresaz@example.com",
                State = "state",
                TIN = "902165455432-52"
            };
            var expectedId = await Result<string>.SuccessAsync("66e9e46ffd4b3a74c70995b3", "");

            // Configurar el mock del servicio para que devuelva un ID esperado
            _supplierServiceMock
                .Setup(service => service.AddAsync(It.IsAny<CreateSupplierCommand>()))
                .ReturnsAsync(expectedId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _supplierServiceMock.Verify(repo => repo.AddAsync(It.IsAny<CreateSupplierCommand>()), Times.Once);
            Assert.Equal(expectedId, result);
        }
    }
}
