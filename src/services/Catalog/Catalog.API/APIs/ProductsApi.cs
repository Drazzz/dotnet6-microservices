namespace Catalog.API;

using Carter;
using Catalog.Domain;

public sealed class ProductsApi : ICarterModule
{
    private static class Routes
    {
        internal const string GetAllProducts = "/api/products";
        internal const string GetProductById = "/api/products/{id:guid}";
        internal const string GetProductByTheCategoryId = "/api/category/{id:int:min(1)}/products";
        internal const string GetProductsByName = "/api/products/{name}";
    }


    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.GetAllProducts, ProductsFeature.GetAllProducts())
            //.RequireAuthorization("policyName1", "policyName2")
            .WithName("GetProducts")
            .Produces<IReadOnlyCollection<Product>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
        ;

        app.MapGet(Routes.GetProductById, ProductsFeature.GetProductById())
            //.RequireAuthorization("policyName1", "policyName2")
            .WithName("GetProduct")
            .Produces<Product>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
        ;

        app.MapGet(Routes.GetProductByTheCategoryId, ProductsFeature.GetProductsByCategoryId())
            //.RequireAuthorization("policyName1", "policyName2")
            .WithName("GetProductByCategory")
            .Produces<IReadOnlyCollection<Product>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
        ;

        app.MapGet(Routes.GetProductsByName, ProductsFeature.GetProductsByName())
            //.RequireAuthorization("policyName1", "policyName2")
            .WithName("GetProductsByName")
            .Produces<IReadOnlyCollection<Product>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
        ;
    }    
}
