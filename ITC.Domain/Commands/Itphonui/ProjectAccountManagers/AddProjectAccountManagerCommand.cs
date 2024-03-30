using System;
using ITC.Domain.Commands.CompanyManagers.StaffManager;
using ITC.Domain.Core.ModelShare.CompanyManager.StaffManagers;

namespace ITC.Domain.Commands.Itphonui.ProjectAccountManagers;

/// <summary>
///     Command thêm quản trị viên hệ thống
/// </summary>
public class AddProjectAccountManagerCommand : StaffManagerCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public AddProjectAccountManagerCommand(ProjectAccountManagerEventModel model)
    {
        Name         = model.Name;
        Description  = model.Description;
        CreateBy     = model.CreatedBy;
        Address      = model.Address;
        Email        = model.Email;
        Phone        = model.Phone;
        AvatarId     = model.AvatarId;
        ProjectId    = model.ProjectId;
        ManagementId = model.ManagementId;
        UserName     = model.UserName;
        AuthorityId  = model.AuthorityId;
    }

#endregion

    public Guid ManagementId { get; set; }

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