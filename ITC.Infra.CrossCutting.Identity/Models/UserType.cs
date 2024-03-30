#region

using System;
using System.Collections.Generic;
using ITC.Domain.Core.Models;
using ITC.Domain.Extensions;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Models;

public class UserType : EntityString
{
#region Constructors

    public UserType()
    {
        ModuleDecentralizations   = new HashSet<ModuleDecentralization>();
        FunctionDecentralizations = new HashSet<FunctionDecentralization>();
        IsDeleted                 = false;
        IsDefault                 = false;
    }

    public UserType(string name, string description, string roleId, UserTarget user, int portalId,
                    string managementUnitId = null) : this()
    {
        Name             = name;
        Description      = description;
        RoleId           = roleId;
        PortalId         = portalId;
        ManagementUnitId = managementUnitId;
        User             = user;
    }

#endregion

#region Properties

    public         string                            Description               { get; set; }
    public virtual HashSet<FunctionDecentralization> FunctionDecentralizations { get; }

    /// <summary>
    ///     Trạng thái mặc định
    /// </summary>
    public bool IsDefault { get; private set; }

    //public ApplicationRole IdentityRole { get; private set; }
    /// <summary>
    ///     Trạng thái xóa
    /// </summary>
    public bool IsDeleted { get; private set; }

    /// <summary>
    ///     Trạng thái quản trị viên cao cấp
    /// </summary>
    public bool IsSuperAdmin { get; private set; }

    /// <summary>
    ///     Id đơn vị quản lý
    /// </summary>
    public string ManagementUnitId { get; }

    public virtual HashSet<ModuleDecentralization> ModuleDecentralizations { get; }

    /// <summary>
    ///     Tên
    /// </summary>
    public string Name { get; private set; }

    public int PortalId { get; }

    /// <summary>
    ///     Mã vai trò
    /// </summary>
    public string RoleId { get; private set; }

    /// <summary>
    ///     Thông tin người chỉnh sửa
    /// </summary>
    public UserTarget User { get; }

#endregion

#region Methods

    public void AddFunctionDecentralizations(List<FunctionDecentralization> functionDecentralizations)
    {
        FunctionDecentralizations.AddRange(functionDecentralizations);
    }

    public void AddModuleDecentralizations(List<ModuleDecentralization> moduleDecentralizations)
    {
        ModuleDecentralizations.AddRange(moduleDecentralizations);
    }

    /// <summary>
    ///     Thay đổi trạng thái mặc định
    /// </summary>
    /// <param name="isDefault"></param>
    public void SetDefault(bool isDefault)
    {
        IsDefault = isDefault;
    }

    /// <summary>
    ///     Thay đổi trạng thái xóa
    /// </summary>
    /// <param name="isDelete"></param>
    public void SetDelete(bool isDelete)
    {
        IsDeleted = isDelete;
    }

    public void SetSuperAdmin(bool isSuperAdmin)
    {
        IsSuperAdmin = isSuperAdmin;
    }


    /// <summary>
    ///     Cập nhật người chỉnh sửa
    /// </summary>
    /// <param name="modifyBy"></param>
    /// <param name="modifyDate"></param>
    public void UpdateUserTager(string modifiedBy, DateTime lastDateModified)
    {
        User.UpdateUserTager(modifiedBy, lastDateModified);
    }

    public void UpdateUserType(string name, string roleId)
    {
        Name   = name;
        RoleId = roleId;
    }

#endregion
}