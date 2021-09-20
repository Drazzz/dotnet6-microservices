using Catalog.API.Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Register services to the container.
builder.Services.AddControllers();
builder.AddSwagger();
builder.AddSerilog();

var app = builder.Build();
var environment = app.Environment;
// Configure the HTTP request pipeline.
app
    .UseExceptionHandling(environment)
    .UseHttpsOnlyOnNonDevelopmentEnvironments(environment)
    .UseSwagger(environment)
    .UseAuthorization()
    .UseAppCors()
    ;

app.MapControllers();

app.Run();
