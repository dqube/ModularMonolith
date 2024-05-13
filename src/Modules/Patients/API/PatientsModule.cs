using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Handlers;
using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Modules;
using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Pagination;
using CompanyName.MyProjectName.BuildingBlocks.Modules;
using CompanyName.MyProjectName.Modules.Patients.Application;
using CompanyName.MyProjectName.Modules.Patients.Application.Patients.Commands;
using CompanyName.MyProjectName.Modules.Patients.Application.Patients.DTO;
using CompanyName.MyProjectName.Modules.Patients.Application.Patients.Queries;
using CompanyName.MyProjectName.Modules.Patients.Domain;
using CompanyName.MyProjectName.Modules.Patients.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace CompanyName.MyProjectName.Modules.Patients.API;

internal class PatientsModule : IModule
{
    public string Name { get; } = "Patients";

    public IEnumerable<string> Policies { get; } = new[]
    {
        "transfers", "patients"
    };

    public void Expose(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/patient", async (AddPatient command, IDispatcher dispatcher) =>
        {
            await dispatcher.SendAsync(command);
            return Results.NoContent();
        }).WithTags("Patient").WithName("Add patient");

        endpoints.MapGet("/executingjobs", async (IScheduler scheduler) =>
        {
            var executingJobs = await scheduler.GetCurrentlyExecutingJobs();
            var jobKeys = executingJobs.Select(job => job.JobDetail.Key.Name);

            return Results.Ok(jobKeys);
        }).WithTags("Jobs").WithName("executing jobs");

        // endpoints.MapPut("/patient-spots/{id:guid}", async (int id, ParkingSpot parkingSpot, IParkingSpotsService service) =>
        // {
        //    parkingSpot.Id = id;
        //    await service.UpdateAsync(parkingSpot);
        //    return Results.NoContent();
        // }).WithTags("Parking spots").WithName("Update parking spot");
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
             .Subscribe<GetPatient, PatientDetailsDto>(
                 "patients/get",
                 (query, serviceProvider, cancellationToken)
                     => serviceProvider.GetRequiredService<IQueryDispatcher>().QueryAsync(query, cancellationToken))
             .Subscribe<BrowsePatients, Paged<PatientDto>>(
                 "patients/patients",
                 (query, serviceprovider, cancellationToken)
             => serviceprovider.GetRequiredService<IQueryDispatcher>().QueryAsync(query, cancellationToken));
    }
}