using System;
using System.Collections.Generic;

namespace ITC.Domain.Core.ModelShare.AuthorityManager;

/// <summary>
///     Class truyền dữ liệu chức năng
/// </summary>
public class MenuManagerEventModel : RequestBaseModel
{
    /// <summary>
    ///     Biểu tượng
    /// </summary>
    public string ManagerICon { get; set; }

    /// <summary>
    ///     Tên menu
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Nhóm menu
    /// </summary>
    public int MenuGroupId { get; set; }

    /// <summary>
    ///     Vị trí hiển thị
    /// </summary>
    public int Position { get; set; }

    /// <summary>
    ///     Đường dẫn mặc định
    /// </summary>
    public string Router { get; set; }

    /// <summary>
    ///     Mã cha con
    /// </summary>
    public string ParentId { get; set; }

    /// <summary>
    ///     Giá trị trái
    /// </summary>
    public int MLeft { get; set; }

    /// <summary>
    ///     Giá trị phải
    /// </summary>
    public int MRight { get; set; }

    /// <summary>
    ///     Giá trị quyền
    /// </summary>
    public int PermissionValue { get; set; }

    /// <summary>
    ///     Mã quyền cấp 1
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    ///     Nhãn hiển thị trên menu
    /// </summary>
    public string Label { get; set; }
}

/// <summary>
///     Class truyền dữ liệu phân quyền
/// </summary>
public class AuthoritiesMenuManagerEventModel
{
    public List<MenuByAuthoritiesSaveModel> Models        { get; set; }
    public Guid                             ProjectId     { get; set; }
    public Guid                             CompanyId     { get; set; }
    public string                           AuthotitiesId { get; set; }
    public string                           Name          { get; set; }
}