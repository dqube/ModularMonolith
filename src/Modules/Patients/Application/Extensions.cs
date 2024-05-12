using CompanyName.MyProjectName.Modules.Patients.Application.Patients.Commands;
using CompanyName.MyProjectName.Modules.Patients.Application.Patients.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyName.MyProjectName.Modules.Patients.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IValidator<AddPatient>, PatientValidator>();
        return services;
    }
}
