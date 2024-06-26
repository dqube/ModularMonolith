using System.Collections.Concurrent;
using System.Diagnostics;
using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;
using CompanyName.MyProjectName.BuildingBlocks.Contexts;
using CompanyName.MyProjectName.BuildingBlocks.Messaging.Brokers;
using Humanizer;

namespace CompanyName.MyProjectName.BuildingBlocks.Observability.Tracing.Decorators;

internal sealed class MessageBrokerTracingDecorator : IMessageBroker
{
    public const string ActivitySourceName = "message_broker";
    private static readonly ActivitySource ActivitySource = new(ActivitySourceName);
    private static readonly ConcurrentDictionary<Type, string> Names = new();
    private readonly IMessageBroker _messageBroker;
    private readonly IContextProvider _contextProvider;

    public MessageBrokerTracingDecorator(IMessageBroker messageBroker, IContextProvider contextProvider)
    {
        _messageBroker = messageBroker;
        _contextProvider = contextProvider;
    }

    public async Task SendAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : IMessage
    {
        var context = _contextProvider.Current();
        var name = Names.GetOrAdd(typeof(T), message.GetType().Name.Underscore());
        using var activity = ActivitySource.StartActivity("publisher", ActivityKind.Producer, context.ActivityId);
        activity?.SetTag("message", name);
        activity?.SetTag("correlation_id", context.CorrelationId);
        activity?.SetTag("causation_id", context.CausationId);
        await _messageBroker.SendAsync(message, cancellationToken);
    }
}