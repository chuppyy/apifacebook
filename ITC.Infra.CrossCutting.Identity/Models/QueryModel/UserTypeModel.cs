#region

using System;
using System.Collections.Generic;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Models.QueryModel;

public class UserTypeModel
{
#region Properties

    public string                   Description     { get; set; }
    public Dictionary<string, bool> DictionaryRoles { get; set; } = new();

    /// <summary>
    ///     Mã
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    ///     Trạng thái mặc định
    /// </summary>
    public bool IsDefault { get; set; }

    /// <summary>
    ///     Trạng thái quản trị viên cao cấp
    /// </summary>
    public bool IsSuperAdmin { get; set; }

    /// <summary>
    ///     Tên
    /// </summary>
    public string Name { get; set; }

    public int PortalId { get; set; }

    /// <summary>
    ///     Mã vai trò
    /// </summary>
    public string RoleId { get; set; }

    /// <summary>
    ///     Danh sách vai trò
    /// </summary>
    public IEnumerable<CustomRoleUserTypeModel> Roles { get; set; }

    public int TotalRecord { get; set; }

#endregion
}

public class UserTypeDetailModel
{
#region Properties

    public string Description { get; set; }

    /// <summary>
    ///     Mã
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    ///     Trạng thái mặc định
    /// </summary>
    public bool IsDefault { get; set; }


    /// <summary>
    ///     Trạng thái quản trị viên cao cấp
    /// </summary>
    public bool IsSuperAdmin { get; set; }

    /// <summary>
    ///     Tên
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Mã vai trò
    /// </summary>
    public string RoleId { get; set; }

    /// <summary>
    ///     Danh sách vai trò
    /// </summary>
    public IEnumerable<TreeViewNode> Roles { get; set; }

#endregion
}

public sealed class CustomRoleUserTypeModel
{
#region Properties

    /// <summary>
    ///     Mã vai trò
    /// </summary>
    public string RoleId { get; set; }

#endregion
}

public class CustomUserTypeModel
{
#region Properties

    public string BaseRoleIdentity        { get; set; }
    public string BaseUnitUserId          { get; set; }
    public string EducationLevelCode      { get; set; }
    public string Email                   { get; set; }
    public string FullName                { get; set; }
    public bool   IsDepartmentOfEducation { get; set; }
    public string ManagementName          { get; set; }

    /// <summary>
    ///     Danh sách quyền
    /// </summary>
    public IEnumerable<CustomModuleModel> Permissions { get; set; }

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

    public string Province     { get; set; }
    public string ProvinceType { get; set; }

#endregion
}

public sealed class CustomManagement
{
#region Properties

    public string EducationLevelCode { get; set; }
    public Guid   Id                 { get; set; }
    public string ManagementName     { get; set; }
    public string UserId             { get; set; }

#endregion
}

public class UserTypeByUserIdDto
{
    public string UserName        { get; set; }
    public Guid   Id              { get; set; }
    public string UserTypeName    { get; set; }
    public string FeatureId       { get; set; }
    public string FeatureDfName   { get; set; }
    public int    PermissionValue { get; set; }
}