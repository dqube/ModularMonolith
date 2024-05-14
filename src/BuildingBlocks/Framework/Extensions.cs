using CompanyName.MyProjectName.BuildingBlocks.Abstractions;
using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Dispatchers;
using CompanyName.MyProjectName.BuildingBlocks.API;
using CompanyName.MyProjectName.BuildingBlocks.API.CORS;
using CompanyName.MyProjectName.BuildingBlocks.API.Exceptions;
using CompanyName.MyProjectName.BuildingBlocks.API.Networking;
using CompanyName.MyProjectName.BuildingBlocks.API.Swagger;
using CompanyName.MyProjectName.BuildingBlocks.API.Validations;
using CompanyName.MyProjectName.BuildingBlocks.Contexts;
using CompanyName.MyProjectName.BuildingBlocks.HTTP;
using CompanyName.MyProjectName.BuildingBlocks.Jobs;
using CompanyName.MyProjectName.BuildingBlocks.Messaging;
using CompanyName.MyProjectName.BuildingBlocks.Modules;
using CompanyName.MyProjectName.BuildingBlocks.Modules.Modules;
using CompanyName.MyProjectName.BuildingBlocks.Observability;
using CompanyName.MyProjectName.BuildingBlocks.Observability.Logging;
using CompanyName.MyProjectName.BuildingBlocks.Observability.Metrics;
using CompanyName.MyProjectName.BuildingBlocks.Observability.Tracing;
using CompanyName.MyProjectName.BuildingBlocks.Security;
using CompanyName.MyProjectName.BuildingBlocks.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyName.MyProjectName.BuildingBlocks.Framework;

public static class Extensions
{
    public static WebApplicationBuilder AddMicroFramework(this WebApplicationBuilder builder)
    {
        // builder.AddVault();
        var appOptions = builder.Configuration.GetSection("app").BindOptions<AppOptions>();
        var appInfo = new AppInfo(appOptions.Name, appOptions.Version);
        builder.Services.AddSingleton(appInfo);

        RenderLogo(appOptions);

        builder
            .AddLogging().Services
            .AddErrorHandling()
            .AddHandlers(appOptions.Project)
            .AddDispatchers()
            .AddContexts()
            .AddMemoryCache()
            .AddHttpContextAccessor()
            .AddMicro(builder.Configuration)

            // .AddAuth(builder.Configuration)
            .AddCorsPolicy(builder.Configuration)
            .AddSwaggerDocs(builder.Configuration)
            .AddHeadersForwarding(builder.Configuration)
            .AddMessaging(builder.Configuration)

            // .AddMetrics(builder.Configuration)
            // .AddTracing(builder.Configuration)
            // .AddConsul(builder.Configuration)
            // .AddFabio(builder.Configuration)
            // .AddSecurity(builder.Configuration)
            .AddLogger(builder.Configuration);

        builder.Services
            .AddHttpClient(builder.Configuration)
            .AddContextHandler();

        // .AddVaultCertificatesHandler(builder.Configuration);
        // .AddConsulHandler()
        // .AddFabioHandler();

        // builder.Services
        //     .AddMessagingMetricsDecorators()
        //     .AddMessagingTracingDecorators();
        return builder;
    }

    public static WebApplication UseMicroFramework(this WebApplication app)
    {
        app
            .UseHeadersForwarding()
            .UseCorsPolicy()
            .UseErrorHandling()
            .UseSwaggerDocs()

            // .UseAuthentication()
            .UseRouting()

            // .UseMetrics()
            .UseAuthorization()
            .UseContexts()
            .UseContextLogger();

        return app;
    }

    public static WebApplicationBuilder AddModularFramework(this WebApplicationBuilder builder)
    {
        // builder.Host.ConfigureModules();
        var configuration = builder.Configuration;
        var modulePart = configuration.BindOptions<AppOptions>("app").ModulePart;
        var assemblies = ModuleLoader.LoadAssemblies(builder.Configuration, modulePart);
        var modules = ModuleLoader.LoadModules(assemblies, modulePart);
        var disabledModules = new List<string>();
        using (var serviceProvider = builder.Services.BuildServiceProvider())
        {
            foreach (var (key, value) in configuration.AsEnumerable())
            {
                if (!key.Contains(":module:enabled"))
                {
                    continue;
                }

                if (!bool.Parse(value))
                {
                    disabledModules.Add(key.Split(":")[0]);
                }
            }
        }

        foreach (var module in modules)
        {
            module.Register(builder.Services, configuration);
        }

        var appOptions = builder.Configuration.GetSection("app").BindOptions<AppOptions>();
        var appInfo = new AppInfo(appOptions.Name, appOptions.Version);
        builder.Services.AddSingleton(appInfo);

        RenderLogo(appOptions);

        builder
            .AddLogging()
            .Services
            .AddMemoryCache()
            .AddErrorHandling()
            .AddModuleInfo(modules)
            .AddModuleRequests(assemblies)
            .AddHandlers(appOptions.Project, assemblies)
            .AddDispatchers()
            .AddContexts()

            // .AddTransactionalDecorators()
            .AddHttpContextAccessor()
            .AddMicro(builder.Configuration)
            .AddStorage(builder.Configuration)

            .AddValidations(builder.Configuration, assemblies)
            .AddCorsPolicy(builder.Configuration)
            .AddSwaggerDocs(builder.Configuration)
            .AddHealthCheck(builder.Configuration)
            .AddMetrics(builder.Configuration)
            .AddTracing(builder.Configuration)
            .AddHeadersForwarding(builder.Configuration)
            .AddMemoryMessaging(builder.Configuration)
            .AddSecurity(builder.Configuration)
            .AddJobs(builder.Configuration, appOptions.ModulePart, assemblies)
            .AddLogger(builder.Configuration);

        // .AddObservability(builder.Configuration)
        builder.Services
           .AddHttpClient(builder.Configuration);
        builder.Services.AddControllers()
            .ConfigureApplicationPartManager(manager =>
            {
                var removedParts = new List<ApplicationPart>();
                foreach (var disabledModule in disabledModules)
                {
                    var parts = manager.ApplicationParts.Where(x => x.Name.Contains(
                        disabledModule,
                        StringComparison.InvariantCultureIgnoreCase));
                    removedParts.AddRange(parts);
                }

                foreach (var part in removedParts)
                {
                    manager.ApplicationParts.Remove(part);
                }

                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });

        assemblies.Clear();
        modules.Clear();
        return builder;
    }

    public static WebApplication UseModularFramework(this WebApplication app)
    {
        app
            .UseHeadersForwarding()
            .UseCorsPolicy()
            .UseErrorHandling()
            .UseSwaggerDocs()
            .UseAuthentication()
            .UseRouting()
            .UseAuthorization()
            .UseContexts()
            .UseContextLogger();

        var configuration = app.Configuration;
        var modulePart = configuration.BindOptions<AppOptions>("app").ModulePart;
        var assemblies = ModuleLoader.LoadAssemblies(configuration, modulePart);
        var modules = ModuleLoader.LoadModules(assemblies, modulePart);

        foreach (var module in modules)
        {
            module.Use(app);
        }

        foreach (var module in modules)
        {
            module.Expose(app);
        }

        assemblies.Clear();
        modules.Clear();
        return app;
    }

    private static void RenderLogo(AppOptions app)
    {
        if (string.IsNullOrWhiteSpace(app.Name))
        {
            return;
        }

        Console.WriteLine($"{app.Name} {app.Version}");
    }
}