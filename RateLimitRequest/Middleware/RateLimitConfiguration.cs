#region

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RateLimitRequest.CounterKeyBuilders;
using RateLimitRequest.Models;
using RateLimitRequest.Resolvers;

#endregion

namespace RateLimitRequest.Middleware;

public class RateLimitConfiguration : IRateLimitConfiguration
{
#region Constructors

    public RateLimitConfiguration(
        IHttpContextAccessor             httpContextAccessor,
        IOptions<IpRateLimitOptions>     ipOptions,
        IOptions<ClientRateLimitOptions> clientOptions)
    {
        IpRateLimitOptions     = ipOptions?.Value;
        ClientRateLimitOptions = clientOptions?.Value;
        HttpContextAccessor    = httpContextAccessor;

        ClientResolvers = new List<IClientResolveContributor>();
        IpResolvers     = new List<IIpResolveContributor>();

        RegisterResolvers();
    }

#endregion

#region Methods

    protected virtual void RegisterResolvers()
    {
        if (!string.IsNullOrEmpty(ClientRateLimitOptions?.ClientIdHeader))
            ClientResolvers.Add(new ClientHeaderResolveContributor(HttpContextAccessor,
                                                                   ClientRateLimitOptions.ClientIdHeader));

        // the contributors are resolved in the order of their collection index
        if (!string.IsNullOrEmpty(IpRateLimitOptions?.RealIpHeader))
            IpResolvers.Add(new IpHeaderResolveContributor(HttpContextAccessor, IpRateLimitOptions.RealIpHeader));

        IpResolvers.Add(new IpConnectionResolveContributor(HttpContextAccessor));
    }

#endregion

#region Fields

    protected readonly ClientRateLimitOptions ClientRateLimitOptions;
    protected readonly IHttpContextAccessor   HttpContextAccessor;

    protected readonly IpRateLimitOptions IpRateLimitOptions;

#endregion

#region IRateLimitConfiguration Members

    public IList<IClientResolveContributor> ClientResolvers { get; } = new List<IClientResolveContributor>();
    public IList<IIpResolveContributor>     IpResolvers     { get; } = new List<IIpResolveContributor>();

    public virtual ICounterKeyBuilder EndpointCounterKeyBuilder { get; } = new PathCounterKeyBuilder();

    public virtual Func<double> RateIncrementer { get; } = () => 1;

#endregion
}