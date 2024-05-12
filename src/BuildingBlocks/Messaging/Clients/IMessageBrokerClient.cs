using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;

namespace CompanyName.MyProjectName.BuildingBlocks.Messaging.Clients;

public interface IMessageBrokerClient
{
    Task SendAsync<T>(
        MessageEnvelope<T> messageEnvelope,
        CancellationToken cancellationToken = default)
        where T : IMessage;
}