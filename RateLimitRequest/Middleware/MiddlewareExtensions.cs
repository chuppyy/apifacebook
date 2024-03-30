#region

using Microsoft.AspNetCore.Builder;

#endregion

namespace RateLimitRequest.Middleware;

public static class MiddlewareExtensions
{
#region Methods

    public static IApplicationBuilder UseClientRateLimiting(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ClientRateLimitMiddleware>();
    }

    public static IApplicationBuilder UseIpRateLimiting(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<IpRateLimitMiddleware>();
    }

#endregion
}