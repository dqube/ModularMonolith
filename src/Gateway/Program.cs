using CompanyName.MyProjectName.BuildingBlocks.Contexts;
using CompanyName.MyProjectName.BuildingBlocks.Framework;
using Yarp.ReverseProxy.Transforms;

var builder = WebApplication
    .CreateBuilder(args)
    .AddMicroFramework();

builder.Configuration.AddJsonFile("yarp.json", false);
builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetRequiredSection("reverseProxy"))
    .AddTransforms(builderContext =>
    {
        builderContext.AddRequestTransform(transformContext =>
        {
            var correlationId = transformContext.HttpContext.GetCorrelationId() ?? Guid.NewGuid().ToString("N");
            transformContext.ProxyRequest.Headers.Add("correlation-id", correlationId);
            return ValueTask.CompletedTask;
        });
    });

var app = builder.Build();

app.MapGet("/", (AppInfo appInfo) => appInfo).WithTags("API").WithName("Info");

app.MapGet("/ping", () => "pong").WithTags("API").WithName("Pong");

#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseMicroFramework()
    .UseEndpoints(x => x.MapReverseProxy());
#pragma warning restore ASP0014 // Suggest using top level route registrations

app.Run();