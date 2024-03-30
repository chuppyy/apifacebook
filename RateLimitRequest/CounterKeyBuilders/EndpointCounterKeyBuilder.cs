using RateLimitRequest.Models;

namespace RateLimitRequest.CounterKeyBuilders;

public class EndpointCounterKeyBuilder : ICounterKeyBuilder
{
#region ICounterKeyBuilder Members

    public string Build(ClientRequestIdentity requestIdentity, RateLimitRule rule)
    {
        // This will allow to rate limit /api/values/1 and api/values/2 under same counter
        return $"_{rule.Endpoint}";
    }

#endregion
}