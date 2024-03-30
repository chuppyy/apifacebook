#region

using System;
using System.Collections.Generic;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Models.QueryModel;

public class ModuleModel
{
#region Properties

    /// <summary>
    ///     Ngày tạo
    /// </summary>
    public DateTime CreateDate { get; }

    /// <summary>
    ///     Mô tả
    /// </summary>
    public string Description { get; }

    public string DisplayName { get; }
    public string GroupId     { get; set; }
    public string GroupName   { get; set; }
    public string Id          { get; set; }

    /// <summary>
    ///     Định danh
    /// </summary>
    public string Identity { get; }

    /// <summary>
    ///     Trạng thái sử dụng
    /// </summary>
    public bool IsActivation { get; }

    /// <summary>
    ///     Tên
    /// </summary>
    public string Name { get; }

    public string RoleId { get; set; }

    public List<CustomRoleModel> Roles { get; set; }

#endregion
}

public sealed class CustomModuleModel
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

public class NienKhoaModel
{
#region Properties

    /// <summary>
    ///     Trọng số
    /// </summary>
    public int BatDau { get; set; }

    /// <summary>
    ///     Mã
    /// </summary>
    public Guid Id { get; set; }

    public int KetThuc { get; set; }

#endregion
}