using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;

namespace CompanyName.MyProjectName.BuildingBlocks.Observability;

#nullable enable
public static class Extensions
{
    private const string OtlpEndpoint = "Otlp:Endpoint";
    private const string OtlpServiceName = "Otlp:ServiceName";

    public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
                .AddUrlGroup(new Uri("http://location.api:5500/health"), name: "Location API");
        return services;
    }
}
