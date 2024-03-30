#region

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RateLimitRequest.Models;

#endregion

namespace RateLimitRequest.Core;

public interface IRateLimitProcessor
{
#region Methods

    Task<IEnumerable<RateLimitRule>> GetMatchingRulesAsync(ClientRequestIdentity identity,
                                                           CancellationToken     cancellationToken = default);

    RateLimitHeaders GetRateLimitHeaders(RateLimitCounter? counter, RateLimitRule rule,
                                         CancellationToken cancellationToken = default);

    bool IsBlacklisted(ClientRequestIdentity requestIdentity);

    bool IsWhitelisted(ClientRequestIdentity requestIdentity);

    Task<RateLimitCounter> ProcessRequestAsync(ClientRequestIdentity requestIdentity, RateLimitRule rule,
                                               CancellationToken     cancellationToken = default);

#endregion
}