#region

using System.Collections.Generic;

#endregion

namespace RateLimitRequest.Models;

public class ClientRateLimitPolicies
{
#region Properties

    public List<ClientRateLimitPolicy> ClientRules { get; set; }

#endregion
}