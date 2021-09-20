namespace Catalog.API.Extensions;

using Serilog;
using Serilog.Events;
using Serilog.Sinks.SpectreConsole;

internal static partial class ServiceCollectionExtensions
{
    /// <summary>
    ///     Configure Serilog Consol logging
    /// </summary>
    public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(nameof(builder));

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.SpectreConsole(
                "{Timestamp:HH:mm:ss} [{Level:u4}] {Message:lj}{NewLine}{Exception}",
                minLevel: LogEventLevel.Information)
            .CreateLogger();

        builder.Host.UseSerilog();
            
        return builder;
    }
}
