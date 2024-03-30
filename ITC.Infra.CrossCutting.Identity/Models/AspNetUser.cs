#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ITC.Domain.Interfaces;
using ITC.Infra.CrossCutting.Identity.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Models;

public class AspNetUser : IUser
{
#region Constructors

    public AspNetUser(IHttpContextAccessor accessor, UserManager<ApplicationUser> userManager)
    {
        _accessor    = accessor;
        _userManager = userManager;
    }

#endregion

#region Fields

    private readonly IHttpContextAccessor         _accessor;
    private readonly UserManager<ApplicationUser> _userManager;

#endregion

#region IUser Members

    public string Name         => GetName();
    public string UserId       => GetUserId();
    public string RoleIdentity => GetRoleDefault();

    public bool   IsSuperAdministrator => GetSuperAdministrator();
    public int    PortalId             => GetPortalId();
    public string Email                => GetEmail();

    public string ManagementUnitId => GetManagementUnit();

    public string UnitUserId => GetUnitUser();
    public string UnitId     => GetUnit();

    public string SchoolYear         => GetchoolYear();
    public string ManagementName     => GetManagementName();
    public string EducationLevelCode => GetEducationLevelCode();
    public string FullName           => GetFullName();
    public string BaseUnitUserId     => GetBaseUserId();
    public string BaseRoleIdentity   => GetBaseRole();
    public string StaffId            => GetStaffId();
    public string StaffName          => GetStaffName();
    public Guid   ProjectId          => GetProjectId();
    public Guid   ManagementId       => GetManagementId();

    public bool IsAuthenticated()
    {
        return _accessor.HttpContext.User.Identity.IsAuthenticated;
    }

    public IEnumerable<Claim> GetClaimsIdentity()
    {
        return _accessor.HttpContext.User.Claims;
    }

    public string GetRoleDefault()
    {
        return _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaimType.RoleDefault)?.Value;
    }

    public IndentityUserModel GetIdentityUser()
    {
        var userClaims = _accessor.HttpContext.User.Claims;
        var userId     = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var userName   = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        // var userType   = "";//userClaims.FirstOrDefault(c => c.Type == CustomClaimType.UserType)?.Value;
        //
        // var spAdmin = bool.Parse(userClaims.FirstOrDefault(c => c.Type == CustomClaimType.SuperAdministrator)?.Value);
        //
        // var roleDefault = userClaims.FirstOrDefault(c => c.Type == CustomClaimType.RoleDefault)?.Value;
        //
        // var managementUnit = _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaimType.ManagementUnit)?.Value ?? userId;
        // var unit = _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaimType.Unit)?.Value;
        // var unitUserId = _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaimType.UnitUser)?.Value ?? userId;
        //
        // var schoolYear = _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaimType.SchoolYear)?.Value;
        // var portalId = int.Parse(_accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaimType.PortalId)?.Value);
        var userModel = new IndentityUserModel
        {
            UserId           = userId,
            UserName         = userName,
            UserTypeId       = "",
            PortalId         = 0,
            RoleDefault      = "",
            IsSuperAdmin     = false,
            ManagementUnitId = "",
            UnitId           = "",
            UnitUserId       = "",
            SchoolYear       = ""
        };
        return userModel;
    }

    public bool IsOfficalAccount()
    {
        return UserId.ToLower() != UnitUserId.ToLower();
    }

#endregion

#region Methods

    private string GetBaseRole()
    {
        var claim =
            _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaimType.BaseRoleDefault);
        if (claim != null) return claim.Value;
        return string.Empty;
    }

    private string GetBaseUserId()
    {
        var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaimType.BaseUnitUserId);
        if (claim != null) return claim.Value;
        return string.Empty;
    }

    private string GetchoolYear()
    {
        var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaimType.SchoolYear);
        if (claim != null) return claim.Value;
        return string.Empty;
    }

    private string GetEducationLevelCode()
    {
        var claim =
            _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaimType.EducationLevelCode);
        if (claim != null) return claim.Value;
        return string.Empty;
    }

    private string GetEmail()
    {
        var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaimType.Email);
        if (claim != null) return claim.Value;
        return string.Empty;
    }

    private string GetFullName()
    {
        var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaimType.FullName);
        if (claim != null) return claim.Value;
        return string.Empty;
    }


    private string GetManagementName()
    {
        var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaimType.ManagementName);
        if (claim != null) return claim.Value;
        return string.Empty;
    }

    private string GetManagementUnit()
    {
        var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaimType.ManagementUnit);
        if (claim != null) return claim.Value;
        return string.Empty;
    }

    /// <summary>
    ///     Trả về StaffId => Mã nhân viên trong bảng StaffManager
    /// </summary>
    /// <returns></returns>
    private string GetStaffId()
    {
        var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "StaffId");
        return claim != null ? claim.Value : string.Empty;
    }

    /// <summary>
    ///     Trả về StaffName => Tên nhân viên trong bảng StaffManager
    /// </summary>
    /// <returns></returns>
    private string GetStaffName()
    {
        var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "StaffName");
        return claim != null ? claim.Value : string.Empty;
    }

    /// <summary>
    ///     Trả về GetProjectId
    /// </summary>
    /// <returns></returns>
    private Guid GetProjectId()
    {
        var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "StaffProjectId");
        if (claim == null) return Guid.Empty;
        Guid.TryParse(claim.Value, out var value);
        return value;
    }

    /// <summary>
    ///     Trả về GetManagementId
    /// </summary>
    /// <returns></returns>
    private Guid GetManagementId()
    {
        var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "StaffManagementId");
        if (claim == null) return Guid.Empty;
        Guid.TryParse(claim.Value, out var value);
        return value;
    }

    private string GetName()
    {
        return _accessor.HttpContext.User.Identity.Name ??
               _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
    }

    private int GetPortalId()
    {
        /*var iPortalIdType = CustomClaimType.PortalId;
        var lClaim      = _accessor.HttpContext.User.Claims.Where(c => c.Type == iPortalIdType).ToList();
        return lClaim.Count > 0 ? Convert.ToInt32(lClaim[0].Value) : 0;*/
        try
        {
            var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaimType.PortalId);
            if (claim != null && int.TryParse(claim.Value, out var portalId)) return portalId;
            return 0;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    private bool GetSuperAdministrator()
    {
        var claim =
            _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaimType.SuperAdministrator);
        if (claim != null && bool.TryParse(claim.Value, out var isSuperAdministrator)) return isSuperAdministrator;
        return false;
    }

    private string GetUnit()
    {
        var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaimType.Unit);
        if (claim != null) return claim.Value;
        return string.Empty;
    }

    private string GetUnitUser()
    {
        var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaimType.UnitUser);
        if (claim != null) return claim.Value;
        return string.Empty;
    }

    private string GetUserId()
    {
        return _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    }

#endregion
}