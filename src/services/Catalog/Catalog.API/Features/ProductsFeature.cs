using Catalog.Domain;

namespace Catalog.API;

internal class ProductsFeature
{
    internal static Func<IProductRepository, CancellationToken, Task<IResult>> GetAllProducts()
    {
        return async (IProductRepository repo, CancellationToken token) =>
        {
            return await repo.GetProducts() switch
            {
                IReadOnlyCollection<Product> products => Results.Ok(products),
                null => Results.NotFound()
            };
        };
    }

    internal static Func<Guid, IProductRepository, CancellationToken, Task<IResult>> GetProductById()
        => async (Guid id, IProductRepository repo, CancellationToken token)
            => await repo.GetProduct(id, token) is Product product
            ? Results.Ok(product)
            : Results.NotFound()
        ;

    internal static Func<int, IProductRepository, CancellationToken, Task<IResult>> GetProductsByCategoryId()
        => async (int id, IProductRepository repo, CancellationToken token)
            => await repo.GetProductsByCategory((CategoryType)id, token) is IReadOnlyCollection<Product> products
            ? Results.Ok(products)
            : Results.NotFound()
        ;

    internal static Func<string, IProductRepository, CancellationToken, Task<IResult>> GetProductsByName()
    {
        return async (string name, IProductRepository repo, CancellationToken token) =>
        {
            if (string.IsNullOrWhiteSpace(name))
                return Results.BadRequest();

            return await repo.GetProductByName(name, token) switch
            {
                IReadOnlyCollection<Product> products => Results.Ok(products),
                null => Results.NotFound()
            };
        };
    }
}
