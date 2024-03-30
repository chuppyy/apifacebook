#region

using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using RateLimitRequest.Models;

#endregion

namespace RateLimitRequest.Store;

public class DistributedCacheIpPolicyStore : DistributedCacheRateLimitStore<IpRateLimitPolicies>, IIpPolicyStore
{
#region Constructors

    public DistributedCacheIpPolicyStore(
        IDistributedCache             cache,
        IOptions<IpRateLimitOptions>  options  = null,
        IOptions<IpRateLimitPolicies> policies = null) : base(cache)
    {
        _options  = options?.Value;
        _policies = policies?.Value;
    }

#endregion

#region IIpPolicyStore Members

    public async Task SeedAsync()
    {
        // on startup, save the IP rules defined in appsettings
        if (_options != null && _policies != null)
            await SetAsync($"{_options.IpPolicyPrefix}", _policies).ConfigureAwait(false);
    }

#endregion

#region Fields

    private readonly IpRateLimitOptions  _options;
    private readonly IpRateLimitPolicies _policies;

#endregion
}