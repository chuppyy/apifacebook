using RateLimitRequest.Models;

namespace RateLimitRequest.CounterKeyBuilders;

public interface ICounterKeyBuilder
{
#region Methods

    string Build(ClientRequestIdentity requestIdentity, RateLimitRule rule);

#endregion
}