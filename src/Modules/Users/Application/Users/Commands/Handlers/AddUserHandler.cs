using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Handlers;
using CompanyName.MyProjectName.Modules.Users.Domain.Users.Entities;
using CompanyName.MyProjectName.Modules.Users.Domain.Users.Repositories;
using Microsoft.Extensions.Logging;

namespace CompanyName.MyProjectName.Modules.Users.Application.Users.Commands.Handlers;

internal sealed class AddUserHandler(IUserRepository patientRepository, ILogger<AddUserHandler> logger) : ICommandHandler<AddUser>
{
    private readonly IUserRepository _patientRepository = patientRepository;
    private readonly ILogger<AddUserHandler> _logger = logger;

    public async Task HandleAsync(AddUser command, CancellationToken cancellationToken = default)
    {
        var patient = User.Create(
           command.UserId,
           command.Name);
        await _patientRepository.AddAsync(patient, cancellationToken);
        _logger.LogInformation($"User {command.UserId} added sucessfully'.");

        // throw new NotImplementedException();
    }
}
