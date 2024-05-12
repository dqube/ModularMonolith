using System.Collections.Concurrent;
using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;
using CompanyName.MyProjectName.BuildingBlocks.Contexts;
using CompanyName.MyProjectName.BuildingBlocks.Messaging;
using CompanyName.MyProjectName.BuildingBlocks.Messaging.Brokers;
using Humanizer;
using Microsoft.Extensions.Logging;

namespace CompanyName.MyProjectName.BuildingBlocks.Transactions.Outbox;

internal sealed class OutboxMessageBroker : IMessageBroker
{
    private readonly ConcurrentDictionary<Type, string> _names = new();
    private readonly IOutbox _outbox;
    private readonly IContextProvider _contextProvider;
    private readonly ILogger<OutboxMessageBroker> _logger;

    public OutboxMessageBroker(IOutbox outbox, IContextProvider contextProvider, ILogger<OutboxMessageBroker> logger)
    {
        _outbox = outbox;
        _contextProvider = contextProvider;
        _logger = logger;
    }

    public async Task SendAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : IMessage
    {
        var messageId = Guid.NewGuid().ToString("N");
        var context = _contextProvider.Current();
        var messageName = _names.GetOrAdd(typeof(T), message.GetType().Name.Underscore());
        _logger.LogInformation(
            "Saving a message to outbox: {MessageName}  [ID: {MessageId}, Activity ID: {ActivityId}]...", messageName, messageId, context.ActivityId);
        var messageEnvelope = new MessageEnvelope<T>(message, new MessageContext(messageId, context));
        await _outbox.SaveAsync(messageEnvelope, cancellationToken);
    }
}