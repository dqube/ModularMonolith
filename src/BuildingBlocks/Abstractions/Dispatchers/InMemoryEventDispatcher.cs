using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;
using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyName.MyProjectName.BuildingBlocks.Abstractions.Dispatchers;

internal sealed class InMemoryEventDispatcher : IEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public InMemoryEventDispatcher(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : class, IEvent
    {
        if (@event is null)
        {
            throw new InvalidOperationException("Event cannot be null.");
        }

        await using var scope = _serviceProvider.CreateAsyncScope();
        var handlers = scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();
        var tasks = handlers.Select(x => x.HandleAsync(@event, cancellationToken));
        await Task.WhenAll(tasks);
    }
}
