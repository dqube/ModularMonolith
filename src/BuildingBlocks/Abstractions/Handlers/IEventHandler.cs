using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;

namespace CompanyName.MyProjectName.BuildingBlocks.Abstractions.Handlers;

public interface IEventHandler<in TEvent>
    where TEvent : class, IEvent
{
    Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
}