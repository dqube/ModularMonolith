using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;

namespace CompanyName.MyProjectName.BuildingBlocks.Abstractions.Handlers;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(IDomainEvent @event, CancellationToken cancellationToken = default);

    Task DispatchAsync(IDomainEvent[] events, CancellationToken cancellationToken = default);
}