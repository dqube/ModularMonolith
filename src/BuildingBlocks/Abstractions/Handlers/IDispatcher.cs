using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;

namespace CompanyName.MyProjectName.BuildingBlocks.Abstractions.Handlers;

public interface IDispatcher
{
    Task SendAsync<T>(T command, CancellationToken cancellationToken = default)
        where T : class, ICommand;

    Task PublishDomainEventAsync<T>(
        T @event,
        CancellationToken cancellationToken = default)
        where T : class, IDomainEvent;

    Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default)
        where T : class, IEvent;

    Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);

    Task<TResult> SendAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default);
}
