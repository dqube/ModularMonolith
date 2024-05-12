using System.Collections.Concurrent;
using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;
using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Attributes;
using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Handlers;
using CompanyName.MyProjectName.BuildingBlocks.Contexts;
using Humanizer;

namespace CompanyName.MyProjectName.BuildingBlocks.Transactions.Inbox;

[Decorator]
internal sealed class InboxEventHandlerDecorator<T> : IEventHandler<T>
    where T : class, IEvent
{
    private static readonly ConcurrentDictionary<Type, string> Names = new();
    private readonly IEventHandler<T> _handler;
    private readonly IContextProvider _contextProvider;
    private readonly IInbox _inbox;

    public InboxEventHandlerDecorator(IEventHandler<T> handler, IContextProvider contextProvider, IInbox inbox)
    {
        _handler = handler;
        _contextProvider = contextProvider;
        _inbox = inbox;
    }

    public Task HandleAsync(T @event, CancellationToken cancellationToken = default)
    {
        var context = _contextProvider.Current();
        var messageName = Names.GetOrAdd(typeof(T), typeof(T).Name.Underscore());
        if (_inbox.Enabled && !string.IsNullOrWhiteSpace(context.MessageId))
        {
#pragma warning disable SA1117 // Parameters should be on same line or separate lines
            return _inbox.HandleAsync(context.MessageId, messageName,
                () => _handler.HandleAsync(
                    @event,
                    cancellationToken), cancellationToken);
#pragma warning restore SA1117 // Parameters should be on same line or separate lines
        }

        return _handler.HandleAsync(@event, cancellationToken);
    }
}