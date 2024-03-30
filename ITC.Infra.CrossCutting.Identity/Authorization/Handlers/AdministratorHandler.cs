#region

using System.Linq;
using System.Threading.Tasks;
using ITC.Infra.CrossCutting.Identity.Authorization.Requirements;
using ITC.Infra.CrossCutting.Identity.Extensions;
using Microsoft.AspNetCore.Authorization;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Authorization.Handlers;

public class AdministratorHandler : AuthorizationHandler<AdministratorRequirement>
{
#region Methods

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   AdministratorRequirement    requirement)
    {
        var ClaimSubperAdmin =
            context.User.Claims.FirstOrDefault(x => x.Type == CustomClaimType.SuperAdministrator);

        if (context.User.IsInRole(requirement.RoleIdentity)       && ClaimSubperAdmin != null &&
            bool.TryParse(ClaimSubperAdmin.Value, out var result) &&
            result == requirement.IsAdministrator) context.Succeed(requirement);

        return Task.CompletedTask;
    }

#endregion
}