#region

using System;
using System.Collections.Generic;
using System.Security.Claims;

#endregion

namespace ITC.Domain.Interfaces;

public interface IUser
{
#region Properties

    string        BaseRoleIdentity { get; }
    public string BaseUnitUserId   { get; }

    /// <summary>
    ///     Mã bậc giáo dục đối với trường
    /// </summary>
    public string EducationLevelCode { get; }

    public string Email                { get; }
    public string FullName             { get; }
    bool          IsSuperAdministrator { get; }

    /// <summary>
    ///     Tên đơn vị quản lý
    /// </summary>
    public string ManagementName { get; }

    /// <summary>
    ///     Đơn vị quản lý
    /// </summary>
    string ManagementUnitId { get; }

    string Name         { get; }
    int    PortalId     { get; }
    string RoleIdentity { get; }

    /// <summary>
    ///     mã niên khóa
    /// </summary>
    string SchoolYear { get; }

    /// <summary>
    ///     mã đơn vị
    /// </summary>
    string UnitId { get; }

    /// <summary>
    ///     Mã người dùng đơn vị quản lý
    /// </summary>
    public string UnitUserId { get; }

    string UserId       { get; }
    string StaffId      { get; }
    string StaffName    { get; }
    Guid   ProjectId    { get; }
    Guid   ManagementId { get; }

#endregion

#region Methods

    IEnumerable<Claim> GetClaimsIdentity();
    IndentityUserModel GetIdentityUser();
    string             GetRoleDefault();

    bool IsAuthenticated();
    bool IsOfficalAccount();

#endregion
}

/// <summary>
///     Thông tin danh tính người dùng
/// </summary>
public class IndentityUserModel
{
#region Properties

    /// <summary>
    ///     quản trị viên là bằng true.
    /// </summary>
    public bool IsSuperAdmin { get; set; }

    public string ManagementUnitId { get; set; }
    public int    PortalId         { get; set; }

    /// <summary>
    ///     Vai trò mặc định
    /// </summary>
    public string RoleDefault { get; set; }

    public string SchoolYear { get; set; }
    public string StandardId { get; set; }
    public string UnitId     { get; set; }
    public string UnitUserId { get; set; }

    /// <summary>
    ///     Mã người dùng
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    ///     Tên đăng nhập
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    ///     Kiểu người dùng
    /// </summary>
    public string UserTypeId { get; set; }

#endregion
}