using System;
using ITC.Domain.Core.ModelShare.AuthorityManager.AuthorityManagerSystems;

namespace ITC.Domain.Commands.AuthorityManager.AuthorityManagerSystem;

/// <summary>
///     Command thêm quyền sử dụng
/// </summary>
public class AddAuthorityManagerSystemCommand : AuthorityManagerSystemCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public AddAuthorityManagerSystemCommand(AuthorityManagerSystemEventModel model)
    {
        Name                    = model.Name;
        Description             = model.Description;
        CreateBy                = model.CreatedBy;
        ProjectId               = model.ProjectId;
        MenuRoleEventViewModels = model.MenuRoleEventViewModels;
    }

#endregion

    public Guid ProjectId { get; set; }

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