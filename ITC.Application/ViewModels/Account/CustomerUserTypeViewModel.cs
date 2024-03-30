#region

using System;
using System.Collections.Generic;

#endregion

namespace ITC.Application.ViewModels.Account;

public class CustomerUserTypeViewModel
{
#region Properties

    public string BaseRoleIdentity   { get; set; }
    public string BaseUnitUserId     { get; set; }
    public string EducationLevelCode { get; set; }
    public string Email              { get; set; }
    public string FullName           { get; set; }
    public string UserName           { get; set; }
    public string ManagementName     { get; set; }

    /// <summary>
    ///     Danh sách quyền
    /// </summary>
    public IEnumerable<CustomModuleViewModel> Permissions { get; set; }

    public int                 PortalId     { get; set; }
    public string              RefreshToken { get; set; }
    public string              RoleId       { get; set; }
    public string              RoleIdentity { get; set; }
    public IEnumerable<string> Roles        { get; set; }
    public Guid                SchoolYear   { get; set; }
    public string              UnitId       { get; set; }
    public string              UnitUserId   { get; set; }
    public string              UserId       { get; set; }
    public string              UserTypeId   { get; set; }
    public string              Province     { get; set; }
    public string              ProvinceType { get; set; }

#endregion
}

public sealed class CustomModuleViewModel
{
#region Properties

    /// <summary>
    ///     Mã
    /// </summary>
    public string Id { get; set; }

    public string RoleIdentity { get; set; }

    /// <summary>
    ///     Trọng số
    /// </summary>
    public int We { get; set; }

#endregion
}