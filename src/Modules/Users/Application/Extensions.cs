using CompanyName.MyProjectName.Modules.Users.Application.Users.Commands;
using CompanyName.MyProjectName.Modules.Users.Application.Users.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyName.MyProjectName.Modules.Users.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IValidator<AddUser>, UserValidator>();
        return services;
    }
}
