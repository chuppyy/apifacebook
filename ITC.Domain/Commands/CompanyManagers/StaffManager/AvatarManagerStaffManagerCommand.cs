using ITC.Domain.Core.ModelShare.CompanyManager.StaffManagers;

namespace ITC.Domain.Commands.CompanyManagers.StaffManager;

/// <summary>
///     Command quản lý ảnh đại diện
/// </summary>
public class AvatarManagerStaffManagerCommand : StaffManagerCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public AvatarManagerStaffManagerCommand(UploadImageStaffEventModel model)
    {
        Id             = model.Id;
        Base64         = model.Base64;
        IsDeleteAvatar = model.IsDeleteAvatar;
    }

#endregion

    /// <summary>
    ///     Đường dẫn ảnh đại diện
    /// </summary>
    public string Base64 { get; set; }

    /// <summary>
    ///     Trạng thái UploadImageEvent
    ///     1. Xóa ảnh đại diện
    ///     2. Thay đổi ảnh đại diện
    /// </summary>
    public int IsDeleteAvatar { get; set; }


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