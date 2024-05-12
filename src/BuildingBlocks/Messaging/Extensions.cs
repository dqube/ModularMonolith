using CompanyName.MyProjectName.BuildingBlocks.Messaging.Brokers;
using CompanyName.MyProjectName.BuildingBlocks.Messaging.Channels;
using CompanyName.MyProjectName.BuildingBlocks.Messaging.Clients;
using CompanyName.MyProjectName.BuildingBlocks.Messaging.Dispatchers;
using CompanyName.MyProjectName.BuildingBlocks.Messaging.Subscribers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyName.MyProjectName.BuildingBlocks.Messaging;

public static class Extensions
{
    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("messaging");
        services.Configure<MessagingOptions>(section);
        services.AddTransient<IMessageBroker, MessageBroker>();
        services.AddSingleton<IMessageBrokerClient, DefaultMessageBrokerClient>();
        services.AddSingleton<IMessageSubscriber, DefaultMessageSubscriber>();

        // services.AddSingleton<IMessagingExceptionPolicyResolver, DefaultMessagingExceptionPolicyResolver>();
        // services.AddSingleton<IMessagingExceptionPolicyHandler, DefaultMessagingExceptionPolicyHandler>();
        return services;
    }

    public static IServiceCollection AddMemoryMessaging(this IServiceCollection services)
    {
        services.AddTransient<IMemoryMessageBroker, MemoryMessageBroker>();
        services.AddTransient<IAsyncEventDispatcher, AsyncEventDispatcher>();
        services.AddSingleton<IEventChannel, EventChannel>();
        services.AddHostedService<EventDispatcherJob>();

        return services;
    }

    public static IMessageSubscriber Subscribe(this IApplicationBuilder app)
        => app.ApplicationServices.GetRequiredService<IMessageSubscriber>();
}
