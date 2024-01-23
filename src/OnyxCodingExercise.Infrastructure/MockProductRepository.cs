using Microsoft.Extensions.Options;
using OnyxCodingExercise.Domain;

namespace OnyxCodingExercise.Infrastructure;

public class MockProductRepository(IOptions<MockProductRepositoryOptions> options)
    : IProductRepository
{
    private readonly Product[] _products = options.Value.Products;

    public Task<Product[]> GetProductsAsync(CancellationToken ct) =>
        Task.FromResult(_products);
}
