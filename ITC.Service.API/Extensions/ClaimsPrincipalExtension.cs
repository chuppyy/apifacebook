#region

using System.Linq;
using System.Security.Claims;
using ITC.Infra.CrossCutting.Identity.Extensions;

#endregion

namespace ITC.Service.API.Extensions;

/// <summary>
/// </summary>
public static class ClaimsPrincipalExtension
{
#region Methods

    /// <summary>
    /// </summary>
    /// <param name="principal"></param>
    /// <returns></returns>
    public static string GetRoleIdentity(this ClaimsPrincipal principal)
    {
        // if (principal.Identity.IsAuthenticated)
        if (principal.Identity is { IsAuthenticated: true })
            return principal.Claims.FirstOrDefault(x => x.Type == CustomClaimType.RoleDefault)?.Value;
        return string.Empty;
    }

#endregion
}