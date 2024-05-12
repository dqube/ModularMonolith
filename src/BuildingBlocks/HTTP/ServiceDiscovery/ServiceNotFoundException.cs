using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Exceptions;

namespace CompanyName.MyProjectName.BuildingBlocks.HTTP.ServiceDiscovery;

internal sealed class ServiceNotFoundException : CustomException
{
    public string Service { get; }

    public ServiceNotFoundException(string service)
        : base($"Service: '{service}' was not found.")
    {
        Service = service;
    }
}