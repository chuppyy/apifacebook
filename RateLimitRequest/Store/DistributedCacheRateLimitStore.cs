#region

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

#endregion

namespace RateLimitRequest.Store;

public class DistributedCacheRateLimitStore<T> : IRateLimitStore<T>
{
#region Fields

    private readonly IDistributedCache _cache;

#endregion

#region Constructors

    public DistributedCacheRateLimitStore(IDistributedCache cache)
    {
        _cache = cache;
    }

#endregion

#region IRateLimitStore<T> Members

    public Task SetAsync(string            id, T entry, TimeSpan? expirationTime = null,
                         CancellationToken cancellationToken = default)
    {
        var options = new DistributedCacheEntryOptions();

        if (expirationTime.HasValue) options.SetAbsoluteExpiration(expirationTime.Value);

        return _cache.SetStringAsync(id, JsonConvert.SerializeObject(entry), options, cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        var stored = await _cache.GetStringAsync(id, cancellationToken);

        return !string.IsNullOrEmpty(stored);
    }

    public async Task<T> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        var stored = await _cache.GetStringAsync(id, cancellationToken);

        if (!string.IsNullOrEmpty(stored)) return JsonConvert.DeserializeObject<T>(stored);

        return default;
    }

    public Task RemoveAsync(string id, CancellationToken cancellationToken = default)
    {
        return _cache.RemoveAsync(id, cancellationToken);
    }

#endregion
}