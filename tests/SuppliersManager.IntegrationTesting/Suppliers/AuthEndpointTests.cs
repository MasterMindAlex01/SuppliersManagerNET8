using SuppliersManager.Api;
using SuppliersManager.Application.Features.Auth.Commands;
using SuppliersManager.Shared.Wrapper;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace SuppliersManager.IntegrationTesting.Suppliers
{
    public class AuthEndpointTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public AuthEndpointTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task LoginToken_ReturnsOkResponse()
        {
            // Arrange
            var newSupplier = new TokenCommand()
            {
                UserName = "alex1234",
                Password = "123456",
            };
            var content = new StringContent(JsonSerializer.Serialize(newSupplier), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/v1/Auth/login", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var result = await response.Content.ReadFromJsonAsync<Result<TokenResponse>>();

            await File.WriteAllTextAsync(@"Imports\token.txt", result!.Data.AccessToken);
            Assert.NotNull(result);
        }
    }
}
