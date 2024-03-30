#region

using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

#endregion

namespace RateLimitRequest.Resolvers;

public class ClientHeaderResolveContributor : IClientResolveContributor
{
#region Constructors

    public ClientHeaderResolveContributor(
        IHttpContextAccessor httpContextAccessor,
        string               headerName)
    {
        _httpContextAccessor = httpContextAccessor;
        _headerName          = headerName;
    }

#endregion

#region IClientResolveContributor Members

    public string ResolveClient()
    {
        string clientId    = null;
        var    httpContext = _httpContextAccessor.HttpContext;

        if (httpContext.User?.Identities.FirstOrDefault() is ClaimsIdentity user)
            clientId                                                                            = user.Name;
        else if (httpContext.Request.Headers.TryGetValue(_headerName, out var values)) clientId = values.First();

        return clientId;
    }

#endregion

#region Fields

    private readonly string               _headerName;
    private readonly IHttpContextAccessor _httpContextAccessor;

#endregion
}