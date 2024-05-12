using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyName.MyProjectName.BuildingBlocks.Modules.Modules;

public sealed class ModuleSubscriber : IModuleSubscriber
{
    private readonly IModuleRegistry _moduleRegistry;
    private readonly IServiceProvider _serviceProvider;

    public ModuleSubscriber(IModuleRegistry moduleRegistry, IServiceProvider serviceProvider)
    {
        _moduleRegistry = moduleRegistry;
        _serviceProvider = serviceProvider;
    }

    public IModuleSubscriber Subscribe<TRequest, TResponse>(
        string path,
        Func<TRequest, IServiceProvider, CancellationToken, Task<TResponse>> action)
        where TRequest : class
        where TResponse : class
    {
#pragma warning disable SA1117 // Parameters should be on same line or separate lines
        _moduleRegistry.AddRequestAction(path, typeof(TRequest), typeof(TResponse),
            async (
                request,
                cancellationToken) =>
            {
                await using var scope = _serviceProvider.CreateAsyncScope();
                return await action((TRequest)request, scope.ServiceProvider, cancellationToken);
            });
#pragma warning restore SA1117 // Parameters should be on same line or separate lines

        return this;
    }

    public IModuleSubscriber Subscribe<TRequest>(
        string path,
        Func<TRequest, IServiceProvider, CancellationToken, Task> action)
        where TRequest : class
    {
#pragma warning disable SA1117 // Parameters should be on same line or separate lines
        _moduleRegistry.AddRequestAction(path, typeof(TRequest), typeof(object),
            async (request, cancellationToken) =>
            {
                await using var scope = _serviceProvider.CreateAsyncScope();
                await action((TRequest)request, scope.ServiceProvider, cancellationToken);
                return default;
            });
#pragma warning restore SA1117 // Parameters should be on same line or separate lines

        return this;
    }
}