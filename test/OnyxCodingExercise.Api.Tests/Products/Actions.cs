using OnyxCodingExercise.Api.Model.Products;

namespace OnyxCodingExercise.Api.Tests.Products;

public static class Actions
{
    public static readonly string Base = "api/products";
    public static string GetProducts() => Base;
    public static string GetProductsByColour(ProductColourModel colour) => $"{Base}/query?colour={colour}";
}
