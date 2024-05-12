using CompanyName.MyProjectName.BuildingBlocks.Framework;
using CompanyName.MyProjectName.BuildingBlocks.Modules;
using CompanyName.MyProjectName.BuildingBlocks.Modules.Modules;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args).AddModularFramework();
var app = builder.Build();
app.MapGet("/", (AppInfo appInfo) => appInfo).WithTags("API").WithName("Info");

app.MapGet("/ping", () => "pong").WithTags("API").WithName("Pong");

app.MapGet("/modules", (ModuleInfoProvider moduleInfoProvider) => moduleInfoProvider.Modules).WithTags("API").WithName("Modules");
app.MapHealthChecks("/health").WithTags("API").WithName("Health");
app.UseModularFramework().Run();