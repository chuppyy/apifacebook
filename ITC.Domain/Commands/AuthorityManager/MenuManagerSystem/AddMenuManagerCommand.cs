namespace ITC.Domain.Commands.AuthorityManager.MenuManagerSystem;

/// <summary>
///     Command thêm chức năng hệ thống
/// </summary>
public class AddMenuManagerCommand : MenuManagerCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="name">Tên danh mục</param>
    /// <param name="description">Mô tả</param>
    /// <param name="parentId">Mã cha con</param>
    /// <param name="managementId">Mã đơn vị</param>
    /// <param name="createBy">Người tạo</param>
    /// <param name="groupId">Mã nhóm dữ liệu</param>
    public AddMenuManagerCommand(string managerICon, string name, int menuGroupId, int position, string router,
                                 string parentId,    int    permissionValue, string code, string createBy)
    {
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