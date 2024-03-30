using System;
using Microsoft.AspNetCore.Http;

namespace ITC.Domain.Core.ModelShare.CompanyManager.StaffManagers;

/// <summary>
///     Event cập nhật ảnh đại diện
/// </summary>
public class UploadImageEventModel
{
    /// <summary>
    ///     Mã Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Dữ liệu ảnh đại diện
    /// </summary>
    public IFormFile FormFiles { get; set; }

    /// <summary>
    ///     Trạng thái UploadImageEvent
    ///     1. Xóa ảnh đại diện
    ///     2. Thay đổi ảnh đại diện
    /// </summary>
    public int IsDeleteAvatar { get; set; }
}