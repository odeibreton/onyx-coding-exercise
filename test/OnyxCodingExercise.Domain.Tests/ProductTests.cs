namespace OnyxCodingExercise.Domain.Tests;

public class ProductTests
{
    [Fact]
    public void GivenValidCreateProductValues_WhenCreating_ShouldCreate()
    {
        var id = 1;
        var name = "Test Product";

        var sut = new Product(id, name);

        sut.Id.Should().Be(id);
        sut.Name.Should().Be(name);
    }

    [Theory]
    [InlineData(1, "")]
    [InlineData(1, " ")]
    public void GivenInvalidCreateProductValues_WhenCreating_ShouldThrow(int id, string name)
    {
        Func<Product> act = () => new Product(id, name);

        act.Should().Throw<ArgumentException>();
    }
}
