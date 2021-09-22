using Catalog.API.Extensions;
using Catalog.Domain;
using Catalog.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Register services to the container.
builder.Services.AddOptions();
builder.AddSwagger();
builder.AddSerilog();
builder.Services.AddCatalogCosmosDB();
builder.Services.AddCatalogRepositories();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddCors();
// Configure services and application
builder.Services.Configure<CosmosDBOptions>(builder.Configuration.GetSection(CosmosDBOptions.SectionName));

var app = builder.Build();
var environment = app.Environment;
// Configure the HTTP request pipeline.
app
    .UseExceptionHandling(environment)
    .UseHttpsOnlyOnNonDevelopmentEnvironments(environment)
    .UseSwagger(environment)
    .UseAuthorization()
    .UseAppCors()
    .EnsureCatalogDatabaseIsCreated(environment)
    ;
app.MapFallback(() => Results.Redirect("/swagger"));

//Minimal API
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



app.Run();
