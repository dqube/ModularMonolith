#nullable enable
using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Storage;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace CompanyName.MyProjectName.BuildingBlocks.Storage.Storage;

internal class RequestStorage : IRequestStorage
{
    private readonly IMemoryCache _cache;
    private readonly StorageOptions _cacheOptions;

    public RequestStorage(IMemoryCache cache, IOptions<StorageOptions> cacheOptions)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _cacheOptions = cacheOptions.Value ?? throw new ArgumentNullException(nameof(cacheOptions));
    }

    public void Set<T>(string key, T value, TimeSpan? duration = null)
    {
        _cache.Set(key, value, duration ?? TimeSpan.FromSeconds(5));
    }

    public T? Get<T>(string key) => _cache.Get<T>(key);

    public async Task<T?> GetAsync<T>(string key) => await Task.FromResult(_cache.Get<T>(key));

    public async Task SetAsync(string key, object data, int? cacheTime = null)
    {
        _cache.Set(key, data, TimeSpan.FromSeconds(cacheTime ?? _cacheOptions.DefaultCacheTime));
        await Task.CompletedTask;
    }

    public void Set(string key, object data, int? cacheTime = null) => _cache.Set(key, data, TimeSpan.FromSeconds(cacheTime ?? _cacheOptions.DefaultCacheTime));

    public async Task<bool> IsSetAsync(string key) => await Task.FromResult(_cache.Get(key) != null);

    public bool IsSet(string key) => _cache.Get(key) != null;

    public async Task RemoveAsync(string key)
    {
        _cache.Remove(key);
        await Task.CompletedTask;
    }

    public void Remove(string key) => _cache.Remove(key);
}
