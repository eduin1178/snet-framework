using Microsoft.VisualStudio.TestPlatform.TestHost;
using SNET.Framework.Domain.Shared;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using SNET.Framework.Features.Users.Commands;
using System.Text.Json;
using System.Net.Http.Json;

namespace SNET.Framework.IntegrationTesting.EndPoints;

public class UserEndPointsTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private const string urlController = "/Users";
    private readonly HttpClient _client;
    private string token = string.Empty;

    public UserEndPointsTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateUser_ReturnsOkResponse()
    {
        // Arrange
        var newSupplier = new CreateUserCommand()
        {
            Email = "test@example.com",
            FirstName = "Test",
            Id = Guid.NewGuid(),
            LastName = "Test",
            Password = "123456",
            PhoneNumber = "3135030716",
        };
        var content = new StringContent(JsonSerializer.Serialize(newSupplier), Encoding.UTF8, "application/json");


        // Act
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _client.PostAsync($"{urlController}", content);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var createdId = await response.Content.ReadFromJsonAsync<Result>();

        Assert.NotNull(createdId);
        Assert.Equal("Usuario creado correctamente", createdId.Message);
    }

}
