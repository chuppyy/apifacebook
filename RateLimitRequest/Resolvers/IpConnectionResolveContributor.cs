#region

using Microsoft.AspNetCore.Http;

#endregion

namespace RateLimitRequest.Resolvers;

public class IpConnectionResolveContributor : IIpResolveContributor
{
#region Fields

    private readonly IHttpContextAccessor _httpContextAccessor;

#endregion

#region Constructors

    public IpConnectionResolveContributor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

#endregion

#region IIpResolveContributor Members

    public string ResolveIp()
    {
        return _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();
    }

#endregion
}