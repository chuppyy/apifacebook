#region

using Microsoft.AspNetCore.Authorization;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Authorization.Requirements;

public class AdministratorRequirement : IAuthorizationRequirement
{
#region Constructors

    public AdministratorRequirement(string roleIdentity, bool isAdministrator)
    {
        RoleIdentity    = roleIdentity;
        IsAdministrator = isAdministrator;
    }

#endregion

#region Properties

    public bool IsAdministrator { get; }

    public string RoleIdentity { get; }

#endregion
}