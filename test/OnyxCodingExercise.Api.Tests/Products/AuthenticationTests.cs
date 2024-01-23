using System.Net;

namespace OnyxCodingExercise.Api.Tests.Products;

public class AuthenticationTests(WebAppFixture webAppFixture)
    : IClassFixture<WebAppFixture>
{
    private readonly HttpClient _httpClient = webAppFixture.HttpClient;

    [Fact]
    public async Task GetProducts_ReturnsUnauthorized()
    {
        var response = await _httpClient.GetAsync(Actions.GetProducts());
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
