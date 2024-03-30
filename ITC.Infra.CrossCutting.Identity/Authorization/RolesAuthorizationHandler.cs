#region

using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Authorization;

public class RolesAuthorizationHandler : AuthorizationHandler<RolesAuthorizationRequirement>, IAuthorizationHandler
{
#region Methods

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext   context,
                                                   RolesAuthorizationRequirement requirement)
    {
        if (context.User == null || !context.User.Identity.IsAuthenticated)
        {
            context.Fail();
            return Task.CompletedTask;
        }

        var validRole = false;
        if (requirement.AllowedRoles       == null ||
            requirement.AllowedRoles.Any() == false)
        {
            validRole = true;
        }
        else
        {
            var claims    = context.User.Claims;
            var userRoles = claims.Where(c => c.Type == ClaimTypes.Role);
            if (userRoles != null)
            {
                var roles = requirement.AllowedRoles;
                userRoles.ToList().Add(userRoles.FirstOrDefault());
                foreach (var role in userRoles)
                {
                    validRole = roles.Contains(role.Value);
                    if (validRole)
                        break;
                }
            }
        }

        if (validRole)
            context.Succeed(requirement);
        else
            context.Fail();
        return Task.CompletedTask;
    }

#endregion
}