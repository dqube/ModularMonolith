using System.Reflection;
using CompanyName.MyProjectName.BuildingBlocks.Abstractions;
using CompanyName.MyProjectName.BuildingBlocks.Messaging.Brokers;
using CompanyName.MyProjectName.BuildingBlocks.Observability.Metrics.Decorators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;

namespace CompanyName.MyProjectName.BuildingBlocks.Observability.Metrics;

public static class Extensions
{
    private const string ConsoleExporter = "console";
    private const string OltpExporter = "oltp";

    public static IServiceCollection AddMetrics(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var section = configuration.GetSection("metrics");
        var options = section.BindOptions<MetricsOptions>();
        services.Configure<MetricsOptions>(section);

        if (!options.Enabled)
        {
            return services;
        }

        services.AddOpenTelemetry()
            .WithMetrics(builder =>
            {
                builder.AddAspNetCoreInstrumentation();
                builder.AddHttpClientInstrumentation();
                builder.AddRuntimeInstrumentation();

                foreach (var attribute in GetMeterAttributes())
                {
                    if (attribute is not null)
                    {
                        builder.AddMeter(attribute.Key);
                    }
                }

                switch (options.Exporter.ToLowerInvariant())
                {
                    case ConsoleExporter:
                        {
                            builder.AddConsoleExporter();
                            break;
                        }

                    case OltpExporter:
                        {
                            var endpoint = options.Endpoint;
                            builder.AddOtlpExporter(prometheus => { prometheus.Endpoint = new Uri(options.Url); });
                            break;
                        }
                }
            });
        return services;
    }

    // public static IApplicationBuilder UseMetrics(this IApplicationBuilder app)
    // {
    //    var metricsOptions = app.ApplicationServices.GetRequiredService<IOptions<MetricsOptions>>().Value;
    //    if (!metricsOptions.Enabled)
    //    {
    //        return app;
    //    }

    // if (metricsOptions.Exporter.ToLowerInvariant() is not OltpExporter)
    //    {
    //        return app;
    //    }

    // app.UseOpenTelemetryPrometheusScrapingEndpoint();

    // return app;
    // }
    public static IServiceCollection AddMessagingMetricsDecorators(this IServiceCollection services)
    {
        services.TryDecorate<IMessageBroker, MessageBrokerMetricsDecorator>();

        return services;
    }

    private static IEnumerable<MeterAttribute> GetMeterAttributes()
        => AppDomain.CurrentDomain.GetAssemblies()
            .Where(x => !x.IsDynamic)
            .SelectMany(x => x.GetTypes())
            .Where(x => x.IsClass && x.GetCustomAttribute<MeterAttribute>() is not null)
            .Select(x => x.GetCustomAttribute<MeterAttribute>())
            .Where(x => x is not null);
}