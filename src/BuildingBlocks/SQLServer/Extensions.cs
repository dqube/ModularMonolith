using CompanyName.MyProjectName.BuildingBlocks.Abstractions;
using CompanyName.MyProjectName.BuildingBlocks.SQLServer.Internals;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyName.MyProjectName.BuildingBlocks.SQLServer;

public static class Extensions
{
    public static IServiceCollection AddSqlServer<T>(this IServiceCollection services, IConfiguration configuration)
        where T : DbContext
    {
        var section = configuration.GetSection("sqlserver");
        var options = section.BindOptions<SQLServerOptions>();
        services.Configure<SQLServerOptions>(section);
        if (!section.Exists())
        {
            return services;
        }

        services.AddDbContext<T>(x => x.UseSqlServer(options.ConnectionString));
        services.AddHostedService<DatabaseInitializer<T>>();
        services.AddHostedService<DataInitializer>();
        services.AddScoped<IUnitOfWork, SQLServerUnitOfWork<T>>();

        return services;
    }

    public static IServiceCollection AddMSSqlServer<T>(this IServiceCollection services, string connectionString)
        where T : DbContext
    {
        var options = services.GetOptions<SQLServerOptions>("sqlserver");
        var connection = options.ConnectionStrings[connectionString];
        services.AddDbContext<T>(x => x.UseSqlServer(connection));
        return services;
    }

    public static IServiceCollection AddUnitOfWork<T>(this IServiceCollection services)

       // where T : class, IUnitOfWork
       where T : DbContext
    {
        services.AddScoped<IUnitOfWork, SQLServerUnitOfWork<T>>();
        services.AddScoped<T>();

        // using var serviceProvider = services.BuildServiceProvider();
        // serviceProvider.GetRequiredService<UnitOfWorkTypeRegistry>().Register<T>();
        return services;
    }

    public static IServiceCollection AddInitializer<T>(this IServiceCollection services)
        where T : class, IDataInitializer
        => services.AddTransient<IDataInitializer, T>();
}