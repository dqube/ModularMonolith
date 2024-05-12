using System.Collections.Concurrent;
using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;
using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Attributes;
using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Handlers;
using CompanyName.MyProjectName.BuildingBlocks.Contexts;
using Humanizer;
using Microsoft.Extensions.Logging;

namespace CompanyName.MyProjectName.BuildingBlocks.Observability.Logging.Decorators;

[Decorator]
internal sealed class LoggingEventHandlerDecorator<T> : IEventHandler<T>
    where T : class, IEvent
{
    private static readonly ConcurrentDictionary<Type, string> Names = new();
    private readonly IEventHandler<T> _handler;
    private readonly IContextProvider _contextProvider;
    private readonly ILogger<LoggingEventHandlerDecorator<T>> _logger;

    public LoggingEventHandlerDecorator(
        IEventHandler<T> handler,
        IContextProvider contextProvider,
        ILogger<LoggingEventHandlerDecorator<T>> logger)
    {
        _handler = handler;
        _contextProvider = contextProvider;
        _logger = logger;
    }

    public async Task HandleAsync(T @event, CancellationToken cancellationToken = default)
    {
        var context = _contextProvider.Current();
        var name = Names.GetOrAdd(typeof(T), @event.GetType().Name.Underscore());
        _logger.LogInformation(
            "Handling an event: {EventName} [Trace ID: {TraceId}, Correlation ID: {CorrelationId}, Message ID: {MessageId}, Causation ID: {CausationId}, User ID: {UserId}]...",
            name,
            context.TraceId,
            context.CorrelationId,
            context.MessageId,
            context.CausationId,
            context.UserId ?? string.Empty);
        await _handler.HandleAsync(@event, cancellationToken);
        _logger.LogInformation(
            "Handled an event: {EventName} [Trace ID: {TraceId}, Correlation ID: {CorrelationId}, Message ID: {MessageId}, Causation ID: {CausationId}, User ID: {UserId}]",
            name,
            context.TraceId,
            context.CorrelationId,
            context.MessageId,
            context.CausationId,
            context.UserId ?? string.Empty);
    }
}