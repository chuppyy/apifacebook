#region

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ITC.Infra.CrossCutting.Identity.Extensions;
using ITC.Infra.CrossCutting.Identity.Models.QueryModel;
using Microsoft.AspNetCore.Authorization;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Authorization;

public class CustomAuthorizationHandler : AuthorizationHandler<CustomRequirement>
{
#region Methods

    // Check whether a given MinimumAgeRequirement is satisfied or not for a particular context
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   CustomRequirement           requirement)
    {
        var                tokenClaim = context.User.Claims.FirstOrDefault(x => x.Type == CustomClaimType.Token);
        IEnumerable<Claim> claimModel = null;
        if (tokenClaim != null)
        {
            var tokenValue   = tokenClaim.Value;
            var tokenHandler = new JwtSecurityTokenHandler();
            var token        = tokenHandler.ReadJwtToken(tokenValue);
            /*tokenModels = token.Claims.Where(x => x.Type == CustomClaimType.Permission)
                               .Select(x => JsonConvert.DeserializeObject<CustomModuleModel>(x.Value));*/
            claimModel = token.Claims.Where(x => x.Type == requirement.Module).ToList();
        }
        else
        {
            //tokenModels = context.User.Claims.Where(x => x.Type == "OrganizationalStructureManaging").FirstOrDefault(); 
            //context.User.Claims.Where(x => x.Type == CustomClaimType.Permission).Select(x => JsonConvert.DeserializeObject<CustomModuleModel>(x.Value));
            claimModel = context.User.Claims.Where(x => x.Type == requirement.Module).ToList();
        }

        // if (String.Compare(requirement.Module, "HelperManaging", StringComparison.Ordinal) == 0)
        // {
        //     context.Succeed(requirement);
        //     return Task.CompletedTask;
        // }

        if (claimModel.Any())
        {
            var valuePer = Convert.ToInt32(claimModel.ToList()[0].Value);
            if (HasPermission(requirement.TypeAudit, valuePer))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
        }

        // Kiểm tra nếu như hàm gọi từ controller Helper thì cho phép qua
        // => Yêu cầu trước khi gọi các hàm này là phải đăng nhập
        var iTokenStaff = context.User.Claims.FirstOrDefault(x => x.Type == "StaffId");
        if (iTokenStaff == null) goto end;

        if (string.Compare(iTokenStaff.ToString(), Guid.Empty.ToString(), StringComparison.Ordinal) != 0)
            if (requirement.Module == "Helpers")
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

    end: ;
        context.Fail();
        return Task.CompletedTask;

        /*var roleIdentityDefault = context.User.Claims.FirstOrDefault(x => x.Type == CustomClaimType.RoleDefault)?.Value;

        var tokenModel = HasModule(tokenModels, requirement.Module, roleIdentityDefault);
        if (tokenModel != null)
            if (HasPermission(requirement.TypeAudit, tokenModel.We))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

        return Task.CompletedTask;*/
    }

    private CustomModuleModel HasModule(IEnumerable<CustomModuleModel> roleSource,
                                        string                         moduleIdentity,
                                        string                         roleIdentity = null)
    {
        if (string.IsNullOrEmpty(roleIdentity))
            return roleSource.FirstOrDefault(x => x.Id.ToUpper() == moduleIdentity.ToUpper());

        return roleSource.FirstOrDefault(x => x.Id.ToUpper()           == moduleIdentity.ToUpper() &&
                                              x.RoleIdentity.ToUpper() == roleIdentity.ToUpper());
    }

    private bool HasPermission(TypeAudit audit, int permission)
    {
        if (((int)audit & permission) == (int)audit)
            return true;

        return false;
    }

#endregion
}

/// <summary>
///     Chức năng
/// </summary>
public enum TypeAudit
{
    View              = 1,
    Add               = 2,
    Edit              = 4,
    Delete            = 8,
    Approved          = 16,
    ViewAllManagement = 32,
    ShowModal         = 64,
    UpdateStatus      = 128,
    ExportFile        = 256,
    ImportFile        = 512,
    ViewDetail        = 1024,
    EditContent       = 2048
}