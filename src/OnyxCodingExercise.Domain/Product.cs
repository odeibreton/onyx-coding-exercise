
namespace OnyxCodingExercise.Domain;

public class Product
{
    public int Id { get; }
    public string Name { get; }

    public Product(int id, string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        Id = id;
        Name = name;
    }
}
