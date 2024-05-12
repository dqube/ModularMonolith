using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Handlers;
using CompanyName.MyProjectName.Modules.Patients.Domain.Patients.Entities;
using CompanyName.MyProjectName.Modules.Patients.Domain.Patients.Repositories;
using Microsoft.Extensions.Logging;

namespace CompanyName.MyProjectName.Modules.Patients.Application.Patients.Commands.Handlers;

internal sealed class UpdatePatientHandler(IPatientRepository patientRepository, ILogger<AddPatientHandler> logger) : ICommandHandler<UpdatePatient>
{
    private readonly IPatientRepository _patientRepository = patientRepository;
    private readonly ILogger<AddPatientHandler> _logger = logger;

    public async Task HandleAsync(UpdatePatient command, CancellationToken cancellationToken = default)
    {
        var patient = Patient.Create(
           command.PatientId,
           command.Name);
        await _patientRepository.UpdateAsync(patient, cancellationToken);
        _logger.LogInformation($"Patient {command.PatientId} updated sucessfully'.");
    }
}
