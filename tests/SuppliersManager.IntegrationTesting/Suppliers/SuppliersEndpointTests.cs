using SuppliersManager.Api;
using SuppliersManager.Application.Features.Suppliers.Commands;
using SuppliersManager.Application.Models.Responses.Suppliers;
using SuppliersManager.Shared.Wrapper;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace SuppliersManager.IntegrationTesting.Suppliers
{
    public class SuppliersEndpointTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private const string urlController = "/v1/Suppliers";
        private readonly HttpClient _client;
        private string token = string.Empty;
        public SuppliersEndpointTests(CustomWebApplicationFactory<Program> factory)
        {
            token = JWTHelperFake.GetFakeToken();
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetSupplierById_ReturnsOkResponse()
        {
            // Arrange
            string id = "66e9e3f8fd4b3a74c70995b2";

            // Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync($"{urlController}/GetSupplierById?id={id}");
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadFromJsonAsync<Result<SupplierResponse>>();
            Assert.NotNull(content);
            Assert.NotNull(content.Data);
        }

        [Fact]
        public async Task GetAllSuppliers_ReturnsOkResponse()
        {
            // Arrange

            // Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync($"{urlController}/GetAll?pageNumber=1&pagesize=10");
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadFromJsonAsync<PaginatedResult<SupplierResponse>>();
            Assert.NotEmpty(content!.Data);
        }

        [Fact]
        public async Task CreateSupplier_ReturnsOkResponse()
        {
            // Arrange
            var newSupplier = new CreateSupplierCommand()
            {
                Address = "cra 50 # 45- 50",
                City = "Medellin",
                Email = "alex@example.com",
                RegisteredName = "EmpresaTest",
                State = "Antioquia",
                TIN = "9825255245-45"
            };
            var content = new StringContent(JsonSerializer.Serialize(newSupplier), Encoding.UTF8, "application/json");


            // Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsync($"{urlController}/create", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var createdId = JsonSerializer.Deserialize<Result<string>>(responseContent);
            var result = await Result<string>.SuccessAsync();

            Assert.NotEqual(result, createdId);
        }

        [Fact]
        public async Task UpdateSupplier_ReturnsOkResponse()
        {
            // Arrange
            var newSupplier = new UpdateSupplierCommand()
            {
                Id = "66e9e4c9fd4b3a74c70995b4",
                Address = "cra 50 # 45- 50",
                City = "Medellin",
                Email = "alex@example.com",
                RegisteredName = "EmpresaTest",
                State = "Antioquia",
                IsActive = true,
            };
            var content = new StringContent(JsonSerializer.Serialize(newSupplier), Encoding.UTF8, "application/json");

            // Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PutAsync($"{urlController}/update", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var createdId = JsonSerializer.Deserialize<Result>(responseContent);
            var result = await Result.SuccessAsync();

            Assert.NotEqual(result, createdId);
        }

        [Fact]
        public async Task DeleteSupplier_ReturnsOkResponse()
        {
            // Arrange
            string id = "66e9e4c9fd4b3a74c70995b4";

            // Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.DeleteAsync($"{urlController}/delete?id={id}");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var createdId = JsonSerializer.Deserialize<Result>(responseContent);
            var result = await Result.SuccessAsync();

            Assert.NotEqual(result, createdId);
        }
    }
}
