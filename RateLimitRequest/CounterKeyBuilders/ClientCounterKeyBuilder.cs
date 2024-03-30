using RateLimitRequest.Models;

namespace RateLimitRequest.CounterKeyBuilders;

public class ClientCounterKeyBuilder : ICounterKeyBuilder
{
#region Fields

    private readonly ClientRateLimitOptions _options;

#endregion

#region Constructors

    public ClientCounterKeyBuilder(ClientRateLimitOptions options)
    {
        _options = options;
    }

#endregion

#region ICounterKeyBuilder Members

    public string Build(ClientRequestIdentity requestIdentity, RateLimitRule rule)
    {
        return $"{_options.RateLimitCounterPrefix}_{requestIdentity.ClientId}_{rule.Period}";
    }

#endregion
}