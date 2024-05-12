using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Handlers;
using CompanyName.MyProjectName.BuildingBlocks.Messaging.Channels;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CompanyName.MyProjectName.BuildingBlocks.Messaging.Dispatchers;

internal sealed class EventDispatcherJob(IEventChannel eventChannel, IEventDispatcher eventDispatcher,
    ILogger<EventDispatcherJob> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var @event in eventChannel.Reader.ReadAllAsync(stoppingToken))
        {
            try
            {
                await eventDispatcher.PublishAsync(@event, stoppingToken);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, exception.Message);
            }
        }
    }
}
