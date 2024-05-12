using CompanyName.MyProjectName.BuildingBlocks.Abstractions;
using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Handlers;
using CompanyName.MyProjectName.BuildingBlocks.Observability.Logging.Decorators;
using CompanyName.MyProjectName.BuildingBlocks.Observability.Logging.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Filters;

namespace CompanyName.MyProjectName.BuildingBlocks.Observability.Logging;

#nullable enable
public static class Extensions
{
    private const string ConsoleOutputTemplate = "{Timestamp:HH:mm:ss} [{Level:u3}] {Message}{NewLine}{Exception}";
    private const string FileOutputTemplate = "{Timestamp:HH:mm:ss} [{Level:u3}] ({SourceContext}.{Method}) {Message}{NewLine}{Exception}";
    private const string AppSectionName = "app";
    private const string SerilogSectionName = "serilog";

    public static IServiceCollection AddLogger(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SerilogOptions>(configuration.GetSection(SerilogSectionName));
        services.AddSingleton<ContextLoggingMiddleware>();
        services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));
        services.TryDecorate(typeof(IEventHandler<>), typeof(LoggingEventHandlerDecorator<>));

        return services;
    }

    public static IApplicationBuilder UseContextLogger(this IApplicationBuilder app)
        => app.UseMiddleware<ContextLoggingMiddleware>();

    public static WebApplicationBuilder AddLogging(
        this WebApplicationBuilder builder,
        Action<LoggerConfiguration>? configure = null,
        string loggerSectionName = SerilogSectionName,
        string appSectionName = AppSectionName)
    {
        builder.Host.AddLogging(configure, loggerSectionName, appSectionName);
        return builder;
    }

    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder app)
    {
        app.UseSerilogRequestLogging();

        // app.MapHealthChecks("/health", new HealthCheckOptions
        //    {
        //        AllowCachingResponses = false,
        //        ResultStatusCodes =
        //            {
        //            [HealthStatus.Healthy] = StatusCodes.Status200OK,
        //            [HealthStatus.Degraded] = StatusCodes.Status200OK,
        //            [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
        //            }
        //    });
        return app;
    }

    private static IHostBuilder AddLogging(
        this IHostBuilder builder, Action<LoggerConfiguration>? configure = null, string loggerSectionName = SerilogSectionName, string appSectionName = AppSectionName)
        => builder.UseSerilog((context, loggerConfiguration) =>
        {
            if (string.IsNullOrWhiteSpace(loggerSectionName))
            {
                loggerSectionName = SerilogSectionName;
            }

            if (string.IsNullOrWhiteSpace(appSectionName))
            {
                appSectionName = AppSectionName;
            }

            var appOptions = context.Configuration.BindOptions<AppOptions>(appSectionName);
            var loggerOptions = context.Configuration.BindOptions<SerilogOptions>(loggerSectionName);

            Configure(loggerOptions, appOptions, loggerConfiguration, context.HostingEnvironment.EnvironmentName);
            configure?.Invoke(loggerConfiguration);
        });

    private static void Configure(
        SerilogOptions serilogOptions, AppOptions appOptions, LoggerConfiguration loggerConfiguration, string environmentName)
    {
        var level = GetLogEventLevel(serilogOptions.MinimumLevel);

        loggerConfiguration.Enrich.FromLogContext()
            .MinimumLevel.Is(level)
            .Enrich.WithProperty("Environment", environmentName)
            .Enrich.WithProperty("Application", appOptions.Name)
            .Enrich.WithProperty("Version", appOptions.Version);

        foreach (var (key, value) in serilogOptions.Tags)
        {
            loggerConfiguration.Enrich.WithProperty(key, value);
        }

        foreach (var (key, value) in serilogOptions.Override)
        {
            var logLevel = GetLogEventLevel(value);
            loggerConfiguration.MinimumLevel.Override(key, logLevel);
        }

        serilogOptions.ExcludePaths?.ToList().ForEach(p => loggerConfiguration.Filter
            .ByExcluding(Matching.WithProperty<string>("RequestPath", n => n.EndsWith(p))));

        serilogOptions.ExcludeProperties?.ToList().ForEach(p => loggerConfiguration.Filter
            .ByExcluding(Matching.WithProperty(p)));

        Configure(loggerConfiguration, serilogOptions, appOptions.Name);
    }

    private static void Configure(LoggerConfiguration loggerConfiguration, SerilogOptions options, string applicationName)
    {
        var consoleOptions = options.Console;
        var fileOptions = options.File;
        var seqOptions = options.Seq;

        if (consoleOptions.Enabled)
        {
            loggerConfiguration.WriteTo.Console(outputTemplate: ConsoleOutputTemplate);
        }

        if (fileOptions.Enabled)
        {
            var path = string.IsNullOrWhiteSpace(fileOptions.Path) ? "logs/logs.txt" : fileOptions.Path;
            if (!Enum.TryParse<RollingInterval>(fileOptions.Interval, true, out var interval))
            {
                interval = RollingInterval.Day;
            }

            loggerConfiguration.WriteTo.File(path, rollingInterval: interval, outputTemplate: FileOutputTemplate);
        }

        // if (seqOptions.Enabled)
        // {
        //    loggerConfiguration.WriteTo.Seq(seqOptions.Url, apiKey: seqOptions.ApiKey);
        // }
        if (seqOptions.Enabled)
        {
            loggerConfiguration.WriteTo.OpenTelemetry(options =>
            {
                options.Endpoint = $"{seqOptions.Url}/v1/logs";
                options.Protocol = Serilog.Sinks.OpenTelemetry.OtlpProtocol.Grpc;
                options.ResourceAttributes = new Dictionary<string, object>
                {
                    ["service.name"] = applicationName
                };
            });
        }
    }

    private static LogEventLevel GetLogEventLevel(string level)
        => Enum.TryParse<LogEventLevel>(level, true, out var logLevel)
            ? logLevel
            : LogEventLevel.Information;
}