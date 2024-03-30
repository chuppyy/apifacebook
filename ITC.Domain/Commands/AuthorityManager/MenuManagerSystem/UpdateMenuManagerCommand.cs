using System;

namespace ITC.Domain.Commands.AuthorityManager.MenuManagerSystem;

/// <summary>
///     Command cập nhật chức năng hệ thống
/// </summary>
public class UpdateMenuManagerCommand : MenuManagerCommand
{
    private readonly Guid _projectId;

#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="name">Tên danh mục</param>
    /// <param name="parentId">Mã cha con</param>
    /// <param name="createBy">Người tạo</param>
    public UpdateMenuManagerCommand(Guid   id,       Guid   projectId, string managerICon, string name, int menuGroupId,
                                    int    position, string router,
                                    string parentId, int    permissionValue, string code, string createBy)
    {
        _projectId      = projectId;
        Id              = id;
        ManagerICon     = managerICon;
        MenuGroupId     = menuGroupId;
        Position        = position;
        Router          = router;
        PermissionValue = permissionValue;
        ParentId        = parentId;
        Code            = code;
        Name            = name;
        CreateBy        = createBy;
    }

#endregion

#region Methods

    /// <summary>
    ///     Kiểm tra valid
    /// </summary>
    /// <returns></returns>
    public override bool IsValid()
    {
        return true;
    }

#endregion
}