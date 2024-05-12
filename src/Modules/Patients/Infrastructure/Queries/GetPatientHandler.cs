using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Handlers;
using CompanyName.MyProjectName.Modules.Patients.Application.Patients.DTO;
using CompanyName.MyProjectName.Modules.Patients.Application.Patients.Queries;
using CompanyName.MyProjectName.Modules.Patients.Infrastructure.DAL;
using CompanyName.MyProjectName.Modules.Patients.Infrastructure.DAL.Mappings;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.MyProjectName.Modules.Patients.Infrastructure.Queries;

internal sealed class GetPatientHandler(PatientsDbContext dbContext) : IQueryHandler<GetPatient, PatientDetailsDto>
{
    private readonly PatientsDbContext _dbContext = dbContext;

    public async Task<PatientDetailsDto> HandleAsync(GetPatient query, CancellationToken cancellationToken = default)
    {
        var patient = await _dbContext.Patients
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == query.PatientId, cancellationToken);

#pragma warning disable CS8603 // Possible null reference return.
        return patient?.AsDetailsDto();
#pragma warning restore CS8603 // Possible null reference return.
    }
}