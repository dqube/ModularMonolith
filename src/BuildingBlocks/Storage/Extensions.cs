using CompanyName.MyProjectName.BuildingBlocks.Abstractions;
using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Storage;
using CompanyName.MyProjectName.BuildingBlocks.Storage.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyName.MyProjectName.BuildingBlocks.Storage;

public static class Extensions
{
    public static IServiceCollection AddStorage(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("cache");
        var options = section.BindOptions<StorageOptions>();
        services.Configure<StorageOptions>(section);

        return services
            .AddSingleton<IRequestStorage, RequestStorage>();
    }
}