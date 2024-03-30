#region

using Microsoft.AspNetCore.Authorization;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Authorization;

public class ClaimRequirement : IAuthorizationRequirement
{
#region Constructors

    public ClaimRequirement(string claimName, string claimValue)
    {
        ClaimName  = claimName;
        ClaimValue = claimValue;
    }

#endregion

#region Properties

    public string ClaimName  { get; set; }
    public string ClaimValue { get; set; }

#endregion
}