#region

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RateLimitRequest.CounterKeyBuilders;
using RateLimitRequest.Middleware;
using RateLimitRequest.Models;
using RateLimitRequest.Store;

#endregion

namespace RateLimitRequest.Core;

public class ClientRateLimitProcessor : RateLimitProcessor, IRateLimitProcessor
{
#region Constructors

    public ClientRateLimitProcessor(
        ClientRateLimitOptions  options,
        IRateLimitCounterStore  counterStore,
        IClientPolicyStore      policyStore,
        IRateLimitConfiguration config)
        : base(options, counterStore, new ClientCounterKeyBuilder(options), config)
    {
        _options     = options;
        _policyStore = policyStore;
    }

#endregion

#region IRateLimitProcessor Members

    public async Task<IEnumerable<RateLimitRule>> GetMatchingRulesAsync(
        ClientRequestIdentity identity, CancellationToken cancellationToken = default)
    {
        var policy =
            await _policyStore.GetAsync($"{_options.ClientPolicyPrefix}_{identity.ClientId}", cancellationToken);

        return GetMatchingRules(identity, policy?.Rules);
    }

#endregion

#region Fields

    private readonly ClientRateLimitOptions                 _options;
    private readonly IRateLimitStore<ClientRateLimitPolicy> _policyStore;

#endregion
}