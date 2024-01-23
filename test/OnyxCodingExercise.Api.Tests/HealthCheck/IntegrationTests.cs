using System.Net;

namespace OnyxCodingExercise.Api.Tests.HealthCheck;

public class IntegrationTests(WebAppFixture webAppFixture)
    : IClassFixture<WebAppFixture>
{
    private readonly HttpClient _httpClient = webAppFixture.HttpClient;

    [Fact]
    public async void GivenANewAPI_WhenCallingHealthCheck_ShouldReturnOK()
    {
        var result = await _httpClient.GetAsync(Actions.HealthCheck());

        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
