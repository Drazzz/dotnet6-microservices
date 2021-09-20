namespace Catalog.API.Extensions;

internal static partial class ApplicationBuilderExtensions
{
    /// <summary>
    ///     Configure exception handling.
    /// </summary>
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app, IWebHostEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(nameof(app));
        ArgumentNullException.ThrowIfNull(nameof(environment));

        if (environment.IsDevelopment())
            app.UseDeveloperExceptionPage();

        return app;
    }

    /// <summary>
    ///     Enforce HTTPS on non-DEV environments
    /// </summary>
    public static IApplicationBuilder UseHttpsOnlyOnNonDevelopmentEnvironments(this IApplicationBuilder app, IWebHostEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(nameof(app));
        ArgumentNullException.ThrowIfNull(nameof(environment));

        // Require use of HTTPS in above DEV environments
        if (!environment.IsDevelopment())
        {
            app.UseHsts();
            app.UseHttpsRedirection();
        }

        return app;
    }

    ///<summary>
    ///     Configure CORS   
    ///</summary>
    public static IApplicationBuilder UseAppCors(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(nameof(app));

        app.UseCors(p =>
        {
            p.AllowAnyHeader();
            p.AllowAnyOrigin();
        });

        return app;
    }
}
