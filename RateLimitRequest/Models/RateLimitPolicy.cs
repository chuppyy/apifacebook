#region

using System.Collections.Generic;

#endregion

namespace RateLimitRequest.Models;

public class RateLimitPolicy
{
#region Properties

    public List<RateLimitRule> Rules { get; set; } = new();

#endregion
}