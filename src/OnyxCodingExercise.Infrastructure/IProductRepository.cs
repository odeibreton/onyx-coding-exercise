using OnyxCodingExercise.Domain;

namespace OnyxCodingExercise.Infrastructure;

public interface IProductRepository
{
    public Task<Product[]> GetProductsAsync(CancellationToken ct);
    public Task<Product[]> GetProductsByColourAsync(ProductColour colour, CancellationToken ct);
}
