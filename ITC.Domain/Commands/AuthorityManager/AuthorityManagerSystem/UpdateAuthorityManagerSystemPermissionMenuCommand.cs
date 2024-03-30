using System.Collections.Generic;
using ITC.Domain.Core.ModelShare.AuthorityManager.AuthorityManagerSystems;

namespace ITC.Domain.Commands.AuthorityManager.AuthorityManagerSystem;

/// <summary>
///     Command cập nhật quyền sử dụng cho menu
/// </summary>
public class UpdateAuthorityManagerSystemPermissionMenuCommand : AuthorityManagerSystemCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public UpdateAuthorityManagerSystemPermissionMenuCommand(AuthorityManagerSystemUpdatePermissionEventModel model)
    {
        Model              = model.Model;
        IsUpdatePermission = model.IsUpdatePermission;
    }

#endregion

    public List<AuthorityManagerSystemUpdatePermissionEventDto> Model              { get; set; }
    public bool                                                 IsUpdatePermission { get; set; }

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