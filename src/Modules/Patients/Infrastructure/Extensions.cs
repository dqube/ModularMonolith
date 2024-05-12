using CompanyName.MyProjectName.BuildingBlocks.SQLServer;
using CompanyName.MyProjectName.Modules.Patients.Application.Clients.Users;
using CompanyName.MyProjectName.Modules.Patients.Domain.Patients.Repositories;
using CompanyName.MyProjectName.Modules.Patients.Infrastructure.Clients;
using CompanyName.MyProjectName.Modules.Patients.Infrastructure.DAL;
using CompanyName.MyProjectName.Modules.Patients.Infrastructure.DAL.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyName.MyProjectName.Modules.Patients.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSingleton<IUserApiClient, UserApiClient>()
            .AddScoped<IPatientRepository, PatientRepository>()
            .AddMSSqlServer<PatientsDbContext>("patient");

        return services;

        // .AddUnitOfWork<PatientsUnitOfWork>();
    }
}