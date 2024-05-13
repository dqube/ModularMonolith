using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Handlers;
using CompanyName.MyProjectName.Modules.Users.Domain.Users.Entities;
using CompanyName.MyProjectName.Modules.Users.Domain.Users.Repositories;
using Microsoft.Extensions.Logging;

namespace CompanyName.MyProjectName.Modules.Users.Application.Users.Commands.Handlers;

internal sealed class UpdateUserHandler(IUserRepository patientRepository, ILogger<AddUserHandler> logger) : ICommandHandler<UpdateUser>
{
    private readonly IUserRepository _patientRepository = patientRepository;
    private readonly ILogger<AddUserHandler> _logger = logger;

    public async Task HandleAsync(UpdateUser command, CancellationToken cancellationToken = default)
    {
        var patient = User.Create(
           command.UserId,
           command.Name);
        await _patientRepository.UpdateAsync(patient, cancellationToken);
        _logger.LogInformation($"User {command.UserId} updated sucessfully'.");
    }
}
