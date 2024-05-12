using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;

namespace CompanyName.MyProjectName.BuildingBlocks.Abstractions.Handlers;

public interface IDomainEventHandler<in TEvent>
    where TEvent : class, IDomainEvent
{
    Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
}