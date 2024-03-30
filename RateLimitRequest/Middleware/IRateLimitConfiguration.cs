#region

using System;
using System.Collections.Generic;
using RateLimitRequest.CounterKeyBuilders;
using RateLimitRequest.Resolvers;

#endregion

namespace RateLimitRequest.Middleware;

public interface IRateLimitConfiguration
{
#region Properties

    IList<IClientResolveContributor> ClientResolvers { get; }

    ICounterKeyBuilder EndpointCounterKeyBuilder { get; }

    IList<IIpResolveContributor> IpResolvers { get; }

    Func<double> RateIncrementer { get; }

#endregion
}