using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Handlers;
using CompanyName.MyProjectName.Modules.Patients.Application.Clients.Users;
using CompanyName.MyProjectName.Modules.Patients.Domain.Patients.Entities;
using CompanyName.MyProjectName.Modules.Patients.Domain.Patients.Repositories;
using Microsoft.Extensions.Logging;

namespace CompanyName.MyProjectName.Modules.Patients.Application.Patients.Commands.Handlers;

internal sealed class AddPatientHandler(IPatientRepository patientRepository, ILogger<AddPatientHandler> logger, IUserApiClient userApiClient) : ICommandHandler<AddPatient>
{
    private readonly IPatientRepository _patientRepository = patientRepository;
    private readonly ILogger<AddPatientHandler> _logger = logger;
    private readonly IUserApiClient _userApiClient = userApiClient;

    public async Task HandleAsync(AddPatient command, CancellationToken cancellationToken = default)
    {
        var user = await _userApiClient.GetUserAsync(1);
        var patient = Patient.Create(
           command.PatientId,
           command.Name);
        await _patientRepository.AddAsync(patient, cancellationToken);
        _logger.LogInformation($"Patient {command.PatientId} added sucessfully'.");

        // throw new NotImplementedException();
    }
}
