using OnyxCodingExercise.Api.Mapping;
using OnyxCodingExercise.Api.Model.Products;
using OnyxCodingExercise.Domain;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace OnyxCodingExercise.Api.Tests.Products;

public class IntegrationTests(WebAppFixture webAppFixture)
    : IClassFixture<WebAppFixture>
{
    private readonly HttpClient _httpClient = webAppFixture.AuthenticatedHttpClient;
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

    [Theory]
    [InlineData(ProductColourModel.Red)]
    [InlineData(ProductColourModel.Blue)]
    [InlineData(ProductColourModel.Green)]
    public async Task GetProductsByColour_ReturnsSubsetOfProducts(ProductColourModel colour)
    {
        var response = await _httpClient.GetAsync(Actions.GetProductsByColour(colour));
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var products = await response.Content.ReadFromJsonAsync<Product[]>(_jsonOptions);

        products.Should().BeEquivalentTo(_configuredProducts.Where(x => ProductMapper.Map(x.Colour) == colour));
    }
}
