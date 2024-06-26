﻿#region

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RateLimitRequest.CounterKeyBuilders;
using RateLimitRequest.Middleware;
using RateLimitRequest.Models;
using RateLimitRequest.Net;
using RateLimitRequest.Store;

#endregion

namespace RateLimitRequest.Core;

public class IpRateLimitProcessor : RateLimitProcessor, IRateLimitProcessor
{
#region Constructors

    public IpRateLimitProcessor(
        IpRateLimitOptions      options,
        IRateLimitCounterStore  counterStore,
        IIpPolicyStore          policyStore,
        IRateLimitConfiguration config)
        : base(options, counterStore, new IpCounterKeyBuilder(options), config)
    {
        _options     = options;
        _policyStore = policyStore;
    }

#endregion

#region Fields

    private readonly IpRateLimitOptions                   _options;
    private readonly IRateLimitStore<IpRateLimitPolicies> _policyStore;

#endregion

#region IRateLimitProcessor Members

    public async Task<IEnumerable<RateLimitRule>> GetMatchingRulesAsync(
        ClientRequestIdentity identity, CancellationToken cancellationToken = default)
    {
        var policies = await _policyStore.GetAsync($"{_options.IpPolicyPrefix}", cancellationToken);

        var rules = new List<RateLimitRule>();

        if (policies?.IpRules?.Any() == true)
        {
            // search for rules with IP intervals containing client IP
            var matchPolicies = policies.IpRules.Where(r => IpParser.ContainsIp(r.Ip, identity.ClientIp));

            foreach (var item in matchPolicies) rules.AddRange(item.Rules);
        }

        return GetMatchingRules(identity, rules);
    }

    public override bool IsWhitelisted(ClientRequestIdentity requestIdentity)
    {
        if (_options.IpWhitelist != null &&
            IpParser.ContainsIp(_options.IpWhitelist, requestIdentity.ClientIp)) return true;

        return base.IsWhitelisted(requestIdentity);
    }

#endregion
}