using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;

namespace CompanyName.MyProjectName.BuildingBlocks.Messaging.Brokers;

public interface IMessageBroker
{
    Task SendAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : IMessage;
}