#region

using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Http;

#endregion

namespace RateLimitRequest.Net;

public interface IIpAddressParser
{
#region Methods

    bool ContainsIp(string ipRule, string clientIp);

    bool ContainsIp(List<string> ipRules, string clientIp);

    bool ContainsIp(List<string> ipRules, string clientIp, out string rule);

    IPAddress GetClientIp(HttpContext context);

    IPAddress ParseIp(string ipAddress);

#endregion
}