namespace Catalog.API.Extensions;

using Catalog.API.ApplicationServices;

internal static partial class ServiceCollectionExtensions
{
    /// <summary>
    ///     Configure Application Services
    /// </summary>
    public static WebApplicationBuilder ApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddApplicationServices();

        return builder;
    }


    /// <summary>
    ///     Register Application Services
    /// </summary>
    internal static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<ProductService>();

        return services;
    }
}

