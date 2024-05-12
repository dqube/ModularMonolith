using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;
using Microsoft.Extensions.Caching.Memory;

namespace CompanyName.MyProjectName.BuildingBlocks.Contexts.Accessors;

public class MessageContextRegistry : IMessageContextRegistry
{
    private readonly IMemoryCache _cache;

    public MessageContextRegistry(IMemoryCache cache)
    {
        _cache = cache;
    }

    public void Set(IMessage message, MessageContext context)
        => _cache.Set(message, context, new MemoryCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromMinutes(1)
        });
}