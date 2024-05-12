namespace CompanyName.MyProjectName.BuildingBlocks.HTTP.ServiceDiscovery;

public interface IServiceDiscoveryRegistration
{
    IEnumerable<string> Tags { get; }
}