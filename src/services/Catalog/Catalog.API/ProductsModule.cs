using Carter;
using Catalog.Domain;

namespace Catalog.API
{
    public sealed class ProductsModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/products", async (IProductRepository repo, CancellationToken token) =>
            {
                return await repo.GetProducts() switch
                {
                    IReadOnlyCollection<Product> products => Results.Ok(products),
                    null => Results.NotFound()
                };
            })
            //SAME AS: //=> await repo.GetProducts() is IReadOnlyCollection<Product> products ? Results.Ok(products) : Results.NotFound()
            //.RequireAuthorization("policyName1", "policyName2")
            .WithName("GetProducts")
            .Produces<IReadOnlyCollection<Product>>(200)
            .Produces(404)
            ;
        }
    }
}
