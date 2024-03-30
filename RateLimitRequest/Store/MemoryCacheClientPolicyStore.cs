#region

using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using RateLimitRequest.Models;

#endregion

namespace RateLimitRequest.Store;

public class MemoryCacheClientPolicyStore : MemoryCacheRateLimitStore<ClientRateLimitPolicy>, IClientPolicyStore
{
#region Constructors

    public MemoryCacheClientPolicyStore(
        IMemoryCache                      cache,
        IOptions<ClientRateLimitOptions>  options  = null,
        IOptions<ClientRateLimitPolicies> policies = null) : base(cache)
    {
        _options  = options?.Value;
        _policies = policies?.Value;
    }

#endregion

#region IClientPolicyStore Members

    public async Task SeedAsync()
    {
        // on startup, save the IP rules defined in appsettings
        if (_options != null && _policies?.ClientRules != null)
            foreach (var rule in _policies.ClientRules)
                await SetAsync($"{_options.ClientPolicyPrefix}_{rule.ClientId}",
                               new ClientRateLimitPolicy { ClientId = rule.ClientId, Rules = rule.Rules })
                    .ConfigureAwait(false);
    }

#endregion

#region Fields

    private readonly ClientRateLimitOptions  _options;
    private readonly ClientRateLimitPolicies _policies;

#endregion
}