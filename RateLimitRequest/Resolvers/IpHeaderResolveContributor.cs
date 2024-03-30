#region

using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;
using RateLimitRequest.Net;

#endregion

namespace RateLimitRequest.Resolvers;

public class IpHeaderResolveContributor : IIpResolveContributor
{
#region Constructors

    public IpHeaderResolveContributor(
        IHttpContextAccessor httpContextAccessor,
        string               headerName)
    {
        _httpContextAccessor = httpContextAccessor;
        _headerName          = headerName;
    }

#endregion

#region IIpResolveContributor Members

    public string ResolveIp()
    {
        IPAddress clientIp = null;

        var httpContent = _httpContextAccessor.HttpContext;

        if (httpContent.Request.Headers.TryGetValue(_headerName, out var values))
            clientIp = IpAddressUtil.ParseIp(values.Last());

        return clientIp?.ToString();
    }

#endregion

#region Fields

    private readonly string               _headerName;
    private readonly IHttpContextAccessor _httpContextAccessor;

#endregion
}