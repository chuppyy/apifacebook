#region

using System;
using ITC.Domain.Core.Commands;

#endregion

namespace ITC.Domain.Commands.AuthorityManager.MenuManagerSystem;

/// <summary>
///     Command danh sách chức năng
/// </summary>
public abstract class MenuManagerCommand : Command
{
    public Guid Id { get; set; }

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
    ///     Giá trị quyền
    /// </summary>
    public int PermissionValue { get; set; }

    /// <summary>
    ///     Mã quyền cấp 1
    /// </summary>
    public string Code { get; set; }
}