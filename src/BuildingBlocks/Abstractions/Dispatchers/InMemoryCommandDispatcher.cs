using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;
using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyName.MyProjectName.BuildingBlocks.Abstractions.Dispatchers;

internal sealed class InMemoryCommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public InMemoryCommandDispatcher(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public async Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : class, ICommand
    {
        if (command is null)
        {
            throw new InvalidOperationException("Command cannot be null.");
        }

        await using var scope = _serviceProvider.CreateAsyncScope();
        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();
        await handler.HandleAsync(command, cancellationToken);
    }

    public async Task<TResult> SendAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
    {
        if (command is null)
        {
            throw new InvalidOperationException("Query cannot be null.");
        }

        await using var scope = _serviceProvider.CreateAsyncScope();
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
        var handler = scope.ServiceProvider.GetRequiredService(handlerType);
        var method = handlerType.GetMethod(nameof(ICommandHandler<ICommand<TResult>, TResult>.HandleAsync));
        if (method is null)
        {
            throw new InvalidOperationException($"Query handler for '{typeof(TResult).Name}' is invalid.");
        }

#pragma warning disable CS8602
#pragma warning disable CS8600
        return await (Task<TResult>)method.Invoke(handler, new object[] { command, cancellationToken });
#pragma warning restore CS8600
#pragma warning restore CS8602
    }
}
