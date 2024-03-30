using RateLimitRequest.Models;

namespace RateLimitRequest.CounterKeyBuilders;

public class IpCounterKeyBuilder : ICounterKeyBuilder
{
#region Fields

    private readonly IpRateLimitOptions _options;

#endregion

#region Constructors

    public IpCounterKeyBuilder(IpRateLimitOptions options)
    {
        _options = options;
    }

#endregion

#region ICounterKeyBuilder Members

    public string Build(ClientRequestIdentity requestIdentity, RateLimitRule rule)
    {
        return $"{_options.RateLimitCounterPrefix}_{requestIdentity.ClientIp}_{rule.Period}";
    }

#endregion
}