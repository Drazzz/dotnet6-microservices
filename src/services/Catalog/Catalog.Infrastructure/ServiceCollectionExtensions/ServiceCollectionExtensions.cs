using Catalog.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCatalogRepositories(this IServiceCollection services) 
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
