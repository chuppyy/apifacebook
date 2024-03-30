#region

using System.Collections.Generic;

#endregion

namespace RateLimitRequest.Models;

public class IpRateLimitPolicies
{
#region Properties

    public List<IpRateLimitPolicy> IpRules { get; set; } = new();

#endregion
}