using CompanyName.MyProjectName.BuildingBlocks.HTTP.ServiceDiscovery;
using Microsoft.Extensions.Options;

namespace CompanyName.MyProjectName.BuildingBlocks.HTTP.LoadBalancing;

internal sealed class FabioServiceDiscoveryRegistration : IServiceDiscoveryRegistration
{
    public IEnumerable<string> Tags { get; }

    public FabioServiceDiscoveryRegistration(IOptions<ConsulOptions> options)
    {
        var serviceName = options.Value.Service.Name;
        Tags = new[] { $"urlprefix-/{serviceName} strip=/{serviceName}" };
    }
}