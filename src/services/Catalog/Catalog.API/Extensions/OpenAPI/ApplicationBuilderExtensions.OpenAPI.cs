namespace Catalog.API.Extensions;

internal static partial class ApplicationBuilderExtensions
{
    /// <summary>
    ///     Configure Swagger endpoints.
    /// </summary>
    public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, IWebHostEnvironment environment, string? routePrefix = null)
    {
        ArgumentNullException.ThrowIfNull(nameof(app));
        ArgumentNullException.ThrowIfNull(nameof(environment));

        if (environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog.API v1");
                c.RoutePrefix = routePrefix ?? c.RoutePrefix;
            });
        }        

        return app;
    }
}