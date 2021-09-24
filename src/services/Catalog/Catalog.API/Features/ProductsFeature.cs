using Catalog.API.ApplicationServices;
using Catalog.API.Models;
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

    internal static Func<Guid, UpdateProductCommand, ProductService, CancellationToken, Task<IResult>> UpdateProduct()
        => async (Guid id, UpdateProductCommand command, ProductService service, CancellationToken token)
            => await service.UpdateProduct(command, token) is bool result
            ? Results.NoContent()
            : Results.NotFound()
        ;

    internal static Func<AddNewProductCommand, ProductService, CancellationToken, Task<IResult>> AddNewProduct()
    {
        return async (AddNewProductCommand command, ProductService service, CancellationToken token) =>
        {
            var id = await service.AddNewProduct(command, token);
            return id == Guid.Empty
                ? Results.BadRequest()
                : Results.Created($"/api/products/{id}", id);
        };
    }
}