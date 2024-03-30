using ITC.Domain.Core.ModelShare.CompanyManager.StaffManagers;

namespace ITC.Domain.Commands.CompanyManagers.StaffManager;

/// <summary>
///     Command cập nhật nhân viên
/// </summary>
public class UpdateStaffManagerCommand : StaffManagerCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public UpdateStaffManagerCommand(StaffManagerEventModel model)
    {
        Id            = model.Id;
        Name          = model.Name;
        Description   = model.Description;
        CreateBy      = model.CreatedBy;
        Address       = model.Address;
        Email         = model.Email;
        Phone         = model.Phone;
        RoomManagerId = model.RoomManagerId;
        AuthorityId   = model.AuthorityManagerId;
        AvatarId      = model.AvatarId;
        UserCode      = model.UserCode;
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