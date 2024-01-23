using Microsoft.AspNetCore.Mvc;
using OnyxCodingExercise.Api.Mapping;
using OnyxCodingExercise.Api.Model.Products;
using OnyxCodingExercise.Infrastructure;

namespace OnyxCodingExercise.Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController(IProductRepository productRepository) : ControllerBase
{
    private readonly IProductRepository _productRepository = productRepository;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductModel>>> GetProducts(CancellationToken ct)
    {
        var products = await _productRepository.GetProductsAsync(ct);

        var mappedProducts = products.Select(ProductMapper.Map);

        return Ok(mappedProducts);
    }
}
