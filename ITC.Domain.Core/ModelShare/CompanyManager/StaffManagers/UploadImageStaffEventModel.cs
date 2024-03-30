using System;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.CompanyManager.StaffManagers;

/// <summary>
///     Event cập nhật ảnh đại diện
/// </summary>
public class UploadImageStaffEventModel : PublishModal
{
    /// <summary>
    ///     Mã Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Trạng thái UploadImageEvent
    ///     1. Xóa ảnh đại diện
    ///     2. Thay đổi ảnh đại diện
    /// </summary>
    public int IsDeleteAvatar { get; set; }

    /// <summary>
    ///     Đường dẫn ảnh đại diện
    /// </summary>
    public string Base64 { get; set; }
}