using OnyxCodingExercise.Api.Model.Products;
using OnyxCodingExercise.Domain;

namespace OnyxCodingExercise.Api.Mapping;

public static class ProductMapper
{
    public static ProductModel Map(Product model) =>
        new(model.Id, model.Name, Map(model.Colour));

    public static ProductColourModel Map(ProductColour model) => model switch
    {
        ProductColour.Red => ProductColourModel.Red,
        ProductColour.Green => ProductColourModel.Green,
        ProductColour.Blue => ProductColourModel.Blue,
        _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
    };

    public static ProductColour Map(ProductColourModel model) => model switch
    {
        ProductColourModel.Red => ProductColour.Red,
        ProductColourModel.Green => ProductColour.Green,
        ProductColourModel.Blue => ProductColour.Blue,
        _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
    };
}
