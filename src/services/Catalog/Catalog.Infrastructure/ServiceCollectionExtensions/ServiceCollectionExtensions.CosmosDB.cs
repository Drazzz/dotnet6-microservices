using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCatalogCosmosDB(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            services.AddDbContext<CataolgDBContext>(opt =>
            {
#if DEBUG
                opt.EnableSensitiveDataLogging();
                opt.EnableDetailedErrors();
#endif
            });            

            return services;
        }
    }
}
