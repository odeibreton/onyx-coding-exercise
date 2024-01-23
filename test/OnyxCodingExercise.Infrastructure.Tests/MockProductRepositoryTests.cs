using Microsoft.Extensions.Options;
using OnyxCodingExercise.Domain;

namespace OnyxCodingExercise.Infrastructure.Tests;

public class MockProductRepositoryTests
{
    private readonly MockProductRepositoryOptions _emptyOptions = new();
    private readonly MockProductRepositoryOptions _fullOptions = new()
    {
        Products =
        [
            new(1, "First product", ProductColour.Blue),
            new(2, "Second product", ProductColour.Red),
            new(3, "Third product", ProductColour.Red),
            new(4, "Fourth product", ProductColour.Blue),
            new(5, "Fifth product", ProductColour.Green),
            new(6, "Sixth product", ProductColour.Green),
            new(7, "Seventh product", ProductColour.Blue),
        ]
    };

    private readonly MockProductRepository _emptySut;
    private readonly MockProductRepository _fullSut;

    public MockProductRepositoryTests()
    {
        _emptySut = new(Options.Create(_emptyOptions));
        _fullSut = new(Options.Create(_fullOptions));
    }

    [Fact]
    public async Task GivenARepository_WithNoProducts_WhenGettingProducts_ShouldReturnEmtpy()
    {
        var result = await _emptySut.GetProductsAsync(default);

        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GivenARepository_WithProducts_WhenGettingProducts_ShouldReturnAllProducts()
    {
        var result = await _fullSut.GetProductsAsync(default);

        result.Should().BeEquivalentTo(_fullOptions.Products);
    }

    [Theory]
    [InlineData(ProductColour.Blue)]
    [InlineData(ProductColour.Green)]
    [InlineData(ProductColour.Red)]
    public async Task GivenARepository_WithNoProducts_WhenGettingProductsByColour_ShouldReturnEmpty(ProductColour colour)
    {
        var result = await _emptySut.GetProductsByColourAsync(colour, default);

        result.Should().BeEmpty();
    }

    [Theory]
    [InlineData(ProductColour.Blue)]
    [InlineData(ProductColour.Green)]
    [InlineData(ProductColour.Red)]
    public async Task GivenARepository_WithProducts_WhenGettingProductsByColour_ShouldReturnMatchingProducts(ProductColour colour)
    {
        var result = await _fullSut.GetProductsByColourAsync(colour, default);

        result.Should().BeEquivalentTo(_fullOptions.Products.Where(p => p.Colour == colour));
    }
}