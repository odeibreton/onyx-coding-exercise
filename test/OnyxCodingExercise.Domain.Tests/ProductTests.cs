namespace OnyxCodingExercise.Domain.Tests;

public class ProductTests
{
    [Fact]
    public void GivenValidCreateProductValues_WhenCreating_ShouldCreate()
    {
        var id = 1;
        var name = "Test Product";
        var colour = ProductColour.Red;

        var sut = new Product(id, name, colour);

        sut.Id.Should().Be(id);
        sut.Name.Should().Be(name);
        sut.Colour.Should().Be(colour);
    }

    [Theory]
    [InlineData(1, "", ProductColour.Red)]
    [InlineData(1, " ", ProductColour.Blue)]
    public void GivenInvalidCreateProductValues_WhenCreating_ShouldThrow(int id, string name, ProductColour colour)
    {
        Func<Product> act = () => new Product(id, name, colour);

        act.Should().Throw<ArgumentException>();
    }
}
