using System;
using ITC.Domain.Core.Models;

namespace ITC.Domain.Models.AuthorityManager;

/// <summary>
///     Danh sách chức năng
/// </summary>
public class MenuManager : RootModel
{
    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected MenuManager()
    {
    }

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public MenuManager(Guid   id,     Guid   projectId, string managerICon, string name, int menuGroupId, int position,
                       string router, string parentId,  int    permissionValue, string code,
                       string createBy = null) : base(id, createBy)
    {
        ProjectId = projectId;
        MLeft     = 1;
        MRight    = 2;
        Position  = position;
        Version   = 2023;
        Update(managerICon, name, menuGroupId, router, parentId, permissionValue, code, createBy);
    }

    /// <summary>
    ///     Mã dự án
    /// </summary>
    public Guid ProjectId { get; set; }

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

    /// <summary>
    ///     Phiên bản sử dụng
    /// </summary>
    public int Version { get; set; }

    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="managerICon">Icon</param>
    /// <param name="name">Tên menu</param>
    /// <param name="menuGroupId">Nhóm menu</param>
    /// <param name="router">Đường dẫn</param>
    /// <param name="parentId">Mã cha con</param>
    /// <param name="permissionValue">Giá trị quyền</param>
    /// <param name="code">Mã quyền cấp 1</param>
    /// <param name="createdBy">Người tạo</param>
    public void Update(string managerICon,     string name, int    menuGroupId, string router, string parentId,
                       int    permissionValue, string code, string createdBy = null)
    {
        ManagerICon     = managerICon;
        Name            = name;
        MenuGroupId     = menuGroupId;
        Router          = router;
        ParentId        = parentId;
        PermissionValue = permissionValue;
        Code            = code;
        Label           = name;
        Update(createdBy);
    }
}