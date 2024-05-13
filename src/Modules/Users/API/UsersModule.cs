using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Handlers;
using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Modules;
using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Pagination;
using CompanyName.MyProjectName.BuildingBlocks.Modules;
using CompanyName.MyProjectName.Modules.Users.Application;
using CompanyName.MyProjectName.Modules.Users.Application.Users.Commands;
using CompanyName.MyProjectName.Modules.Users.Application.Users.DTO;
using CompanyName.MyProjectName.Modules.Users.Application.Users.Queries;
using CompanyName.MyProjectName.Modules.Users.Domain;
using CompanyName.MyProjectName.Modules.Users.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyName.MyProjectName.Modules.Users.API;

internal class UsersModule : IModule
{
    public string Name { get; } = "Users";

    public IEnumerable<string> Policies { get; } = new[]
    {
        "transfers", "Users"
    };

    public void Expose(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/User", async (AddUser command, IDispatcher dispatcher) =>
        {
            await dispatcher.SendAsync(command);
            return Results.NoContent();
        }).WithTags("User").WithName("Add User");
    }

    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        services
        .AddDomain()
        .AddApplication()
        .AddInfrastructure(configuration);
    }

    public void Use(IApplicationBuilder app)
    {
        app.UseModuleRequests()
             .Subscribe<GetUser, UserDetailsDto>(
                 "Users/get",
                 (query, serviceProvider, cancellationToken)
                     => serviceProvider.GetRequiredService<IQueryDispatcher>().QueryAsync(query, cancellationToken))
             .Subscribe<BrowseUsers, Paged<UserDto>>(
                 "Users/Users",
                 (query, serviceprovider, cancellationToken)
             => serviceprovider.GetRequiredService<IQueryDispatcher>().QueryAsync(query, cancellationToken));
    }
}