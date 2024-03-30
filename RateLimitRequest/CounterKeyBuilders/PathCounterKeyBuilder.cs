using RateLimitRequest.Models;

namespace RateLimitRequest.CounterKeyBuilders;

public class PathCounterKeyBuilder : ICounterKeyBuilder
{
#region ICounterKeyBuilder Members

    public string Build(ClientRequestIdentity requestIdentity, RateLimitRule rule)
    {
        return $"_{requestIdentity.HttpVerb}_{requestIdentity.Path}";
    }

#endregion
}