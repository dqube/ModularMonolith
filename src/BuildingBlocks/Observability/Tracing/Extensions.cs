using CompanyName.MyProjectName.BuildingBlocks.Abstractions;
using CompanyName.MyProjectName.BuildingBlocks.Messaging.Brokers;
using CompanyName.MyProjectName.BuildingBlocks.Observability.Tracing.Decorators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace CompanyName.MyProjectName.BuildingBlocks.Observability.Tracing;

public static class Extensions
{
    private const string ConsoleExporter = "console";
    private const string OltpExporter = "oltp";

    public static IServiceCollection AddTracing(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var tracingSection = configuration.GetSection("tracing");
        var tracingOptions = tracingSection.BindOptions<TracingOptions>();
        services.Configure<TracingOptions>(tracingSection);

        if (!tracingOptions.Enabled)
        {
            return services;
        }

        var appName = configuration.BindOptions<AppOptions>("app").Name;
        if (string.IsNullOrWhiteSpace(appName))
        {
            throw new InvalidOperationException("Application name cannot be empty when using the tracing.");
        }

        services.AddOpenTelemetry()
        .WithTracing(builder =>
        {
            builder.SetResourceBuilder(ResourceBuilder.CreateDefault()
                    .AddTelemetrySdk()
                    .AddEnvironmentVariableDetector()
                    .AddService(appName))
                .AddSource(appName)
                .AddSource(MessageBrokerTracingDecorator.ActivitySourceName)

                // .AddSource(MessageHandlerTracingDecorator.ActivitySourceName)
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation();

                // .AddSqlClientInstrumentation();
            switch (tracingOptions.Exporter.ToLowerInvariant())
            {
                case ConsoleExporter:
                    {
                        builder.AddConsoleExporter();
                        break;
                    }

                case OltpExporter:
                    {
                        var oltpOptions = tracingOptions.Oltp;
                        builder.AddOtlpExporter(oltp =>
                        {
                            oltp.Endpoint = new Uri(tracingOptions.Url);
                        });
                        break;
                    }
            }
        });
        return services;
    }

    public static IServiceCollection AddMessagingTracingDecorators(this IServiceCollection services)
    {
        services.TryDecorate<IMessageBroker, MessageBrokerTracingDecorator>();

       // services.TryDecorate<IMessageHandler, MessageHandlerTracingDecorator>();
        return services;
    }
}