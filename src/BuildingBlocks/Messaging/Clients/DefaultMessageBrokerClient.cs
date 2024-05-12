using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;
using Humanizer;
using Microsoft.Extensions.Logging;

namespace CompanyName.MyProjectName.BuildingBlocks.Messaging.Clients;

internal sealed class DefaultMessageBrokerClient : IMessageBrokerClient
{
    private readonly ILogger<DefaultMessageBrokerClient> _logger;

    public DefaultMessageBrokerClient(ILogger<DefaultMessageBrokerClient> logger)
    {
        _logger = logger;
    }

    Task IMessageBrokerClient.SendAsync<T>(
        MessageEnvelope<T> message,
        CancellationToken cancellationToken)
    {
        if (message is null)
        {
            throw new ArgumentNullException(nameof(message));
        }

        var name = message.GetType().Name.Underscore();
        _logger.LogInformation($"Default message broker, message: '{name}', ID: '{message.Context.MessageId}' won't be sent.");
        return Task.CompletedTask;
    }
}