using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;
using CompanyName.MyProjectName.BuildingBlocks.Contexts;
using CompanyName.MyProjectName.BuildingBlocks.Messaging.Clients;

namespace CompanyName.MyProjectName.BuildingBlocks.Messaging.Brokers;

internal sealed class MessageBroker : IMessageBroker
{
    private readonly IMessageBrokerClient _client;
    private readonly IContextProvider _contextProvider;

    public MessageBroker(IMessageBrokerClient client, IContextProvider contextProvider)
    {
        _client = client;
        _contextProvider = contextProvider;
    }

    public async Task SendAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : IMessage
    {
        var messageId = Guid.NewGuid().ToString("N");
        var context = _contextProvider.Current();
#pragma warning disable SA1117 // Parameters should be on same line or separate lines
        await _client.SendAsync(
            new MessageEnvelope<T>(
                message, new MessageContext(messageId, context)), cancellationToken);
#pragma warning restore SA1117 // Parameters should be on same line or separate lines
    }
}