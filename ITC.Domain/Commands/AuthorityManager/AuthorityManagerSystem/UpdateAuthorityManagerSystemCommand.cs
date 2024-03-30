using ITC.Domain.Core.ModelShare.AuthorityManager.AuthorityManagerSystems;

namespace ITC.Domain.Commands.AuthorityManager.AuthorityManagerSystem;

/// <summary>
///     Command cập nhật quyền sử dụng
/// </summary>
public class UpdateAuthorityManagerSystemCommand : AuthorityManagerSystemCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public UpdateAuthorityManagerSystemCommand(AuthorityManagerSystemEventModel model)
    {
        Id                      = model.Id;
        Name                    = model.Name;
        Description             = model.Description;
        CreateBy                = model.CreatedBy;
        MenuRoleEventViewModels = model.MenuRoleEventViewModels;
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