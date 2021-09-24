namespace Catalog.API;

using Carter;
using Catalog.Domain;

public sealed class ProductsApi : ICarterModule
{
    private static class Routes
    {
        internal const string Products = "/api/products";
        internal const string ProductById = "/api/products/{id:guid}";
        internal const string GetProductByTheCategoryId = "/api/category/{id:int}/products";
        internal const string GetProductsByName = "/api/products/{name}";
    }


    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Products, ProductsFeature.GetAllProducts())
            //.RequireAuthorization("policyName1", "policyName2")
            .WithName("GetProducts")
            .Produces<IReadOnlyCollection<Product>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            ;

        app.MapGet(Routes.ProductById, ProductsFeature.GetProductById())
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

        app.MapPut(Routes.ProductById, ProductsFeature.UpdateProduct())
            //.RequireAuthorization("policyName1", "policyName2")
            .WithName("UpdateProduct")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            ;

        app.MapPost(Routes.Products, ProductsFeature.AddNewProduct())
            .WithName("AddNewProduct")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            ;
    }    
}
