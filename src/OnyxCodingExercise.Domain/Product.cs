
namespace OnyxCodingExercise.Domain;

public class Product
{
    public int Id { get; }
    public string Name { get; }
    public ProductColour Colour { get; }

    public Product(int id, string name, ProductColour colour)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        Id = id;
        Name = name;
        Colour = colour;
    }
}
