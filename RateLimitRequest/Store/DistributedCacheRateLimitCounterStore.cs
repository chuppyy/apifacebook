#region

using Microsoft.Extensions.Caching.Distributed;
using RateLimitRequest.Models;

#endregion

namespace RateLimitRequest.Store;

public class DistributedCacheRateLimitCounterStore : DistributedCacheRateLimitStore<RateLimitCounter?>,
                                                     IRateLimitCounterStore
{
#region Constructors

    public DistributedCacheRateLimitCounterStore(IDistributedCache cache) : base(cache)
    {
    }

#endregion
}