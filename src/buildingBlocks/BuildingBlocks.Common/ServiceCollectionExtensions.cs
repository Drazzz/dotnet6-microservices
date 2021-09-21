using Microsoft.Extensions.DependencyInjection;
using NodaTime;

namespace BuildingBlocks.Common
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            services.AddSingleton<IClock>(s => SystemClock.Instance);

            return services;
        }
    }
}
