using CompanyName.MyProjectName.BuildingBlocks.SQLServer;
using CompanyName.MyProjectName.Modules.Users.Domain.Users.Repositories;
using CompanyName.MyProjectName.Modules.Users.Infrastructure.DAL;
using CompanyName.MyProjectName.Modules.Users.Infrastructure.DAL.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyName.MyProjectName.Modules.Users.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddScoped<IUserRepository, UserRepository>()
            .AddMSSqlServer<UserDbContext>("patient");

        return services;
    }
}