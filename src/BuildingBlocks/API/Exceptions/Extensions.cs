using CompanyName.MyProjectName.BuildingBlocks.API.Exceptions.Mappers;
using CompanyName.MyProjectName.BuildingBlocks.API.Exceptions.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyName.MyProjectName.BuildingBlocks.API.Exceptions;

public static class Extensions
{
    public static IServiceCollection AddErrorHandling(this IServiceCollection services)
        => services
            .AddSingleton<ErrorHandlerMiddleware>()
            .AddSingleton<IExceptionToResponseMapper, ExceptionToResponseMapper>();

    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
        => app.UseMiddleware<ErrorHandlerMiddleware>();
}