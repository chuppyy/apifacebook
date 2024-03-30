#region

using Microsoft.Extensions.Caching.Memory;
using RateLimitRequest.Models;

#endregion

namespace RateLimitRequest.Store;

public class MemoryCacheRateLimitCounterStore : MemoryCacheRateLimitStore<RateLimitCounter?>, IRateLimitCounterStore
{
#region Constructors

    public MemoryCacheRateLimitCounterStore(IMemoryCache cache) : base(cache)
    {
    }

#endregion
}