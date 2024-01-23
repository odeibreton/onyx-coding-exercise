using OnyxCodingExercise.Domain;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace OnyxCodingExercise.Api.Tests.Products;

public class IntegrationTests(WebAppFixture webAppFixture)
    : IClassFixture<WebAppFixture>
{
    private readonly HttpClient _httpClient = webAppFixture.HttpClient;
    private readonly Product[] _configuredProducts = webAppFixture.ConfiguredProducts;
    private readonly JsonSerializerOptions _jsonOptions = webAppFixture.JsonSerializerOptions;

    [Fact]
    public async Task GetProducts_ReturnsAllProducts()
    {
        var response = await _httpClient.GetAsync(Actions.GetProducts());
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var products = await response.Content.ReadFromJsonAsync<Product[]>(_jsonOptions);

        products.Should().BeEquivalentTo(_configuredProducts);
    }
}
