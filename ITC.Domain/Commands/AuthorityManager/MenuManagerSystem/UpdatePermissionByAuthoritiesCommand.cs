using ITC.Domain.Core.ModelShare.AuthorityManager;

namespace ITC.Domain.Commands.AuthorityManager.MenuManagerSystem;

/// <summary>
///     Command cập nhật giá trị quyền
/// </summary>
public class UpdatePermissionByAuthoritiesCommand : AuthoritiesMenuManagerCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public UpdatePermissionByAuthoritiesCommand(SortMenuPermissionByAuthoritiesModel model)
    {
        Id    = model.Id;
        Value = model.Value;
    }

#endregion

    /// <summary>
    ///     Giá trị quyên
    /// </summary>
    public int Value { get; set; }

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