using System;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.SaleProductManagers.ContactManager;

/// <summary>
///     [Model] nhận dữ liệu liên hệ từ FE
/// </summary>
public class ContactManagerEventModel : PublishModal
{
    public Guid Id { get; set; }

    /// <summary>
    ///     Tên liên hệ
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Điện thoại
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    ///     Email
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    ///     Skype
    /// </summary>
    public string Skype { get; set; }

    /// <summary>
    ///     Zalo
    /// </summary>
    public string Zalo { get; set; }

    /// <summary>
    ///     Địa chỉ
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    ///     Thời gian làm việc
    /// </summary>
    public string TimeWork { get; set; }

    /// <summary>
    ///     Mã số thuế
    /// </summary>
    public string TaxNumber { get; set; }

    /// <summary>
    ///     Hiển thị trên trang chủ
    /// </summary>
    public bool IsShowHomePage { get; set; }

    /// <summary>
    ///     Ảnh đại diện
    /// </summary>
    public Guid AvatarId { get; set; }

    /// <summary>
    ///     Bản đồ
    /// </summary>
    public string GoogleMap { get; set; }

    /// <summary>
    ///     Đường dây nóng
    /// </summary>
    public string Hotline { get; set; }

    /// <summary>
    ///     Facebook
    /// </summary>
    public string Facebook { get; set; }

    /// <summary>
    ///     Youtube
    /// </summary>
    public string Youtube { get; set; }

    /// <summary>
    ///     Twitter
    /// </summary>
    public string Twitter { get; set; }

    /// <summary>
    ///     Linkedin
    /// </summary>
    public string Linkedin { get; set; }
}

/// <summary>
///     [Model] Trả về ContactManagerGetByIdModel
/// </summary>
public class ContactManagerGetByIdModel : ContactManagerEventModel
{
    public bool IsLocal { get; set; }
}