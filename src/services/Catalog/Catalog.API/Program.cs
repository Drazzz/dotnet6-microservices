using BuildingBlocks.Common;
using Carter;
using Catalog.API.Extensions;
using Catalog.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Register services to the container.
builder.Services.AddCarter();
builder.Services.AddOptions();
builder.AddSwagger();
builder.AddSerilog();
builder.Services.AddCommonServices();
builder.ApplicationServices();
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
app.MapCarter();

app.Run();
