#region

using System.Linq;
using System.Security.Claims;

#endregion

namespace ITC.Domain.Core.Extensions;

public static class ClaimsPrincipalExtensions
{
#region Methods

    /// <summary>
    ///     Get a claim from claimsPrincipal.Claims with specified criteria.
    /// </summary>
    /// <param name="claimsPrincipal">Encapsulates principals that are user accounts.</param>
    /// <param name="claimType">Claim's type</param>
    /// <returns>Claim with the specify type</returns>
    public static Claim GetClaim(this ClaimsPrincipal claimsPrincipal, string claimType)
    {
        return claimsPrincipal.Claims.FirstOrDefault(x => x.Type?.ToUpper() == claimType?.ToUpper());
    }

#endregion
}