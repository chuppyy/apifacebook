using System;
using ITC.Domain.Core.Models;

namespace ITC.Domain.Models.HomeManagers;

/// <summary>
///     Cấu hình trang liên hệ
/// </summary>
public class HomeContact : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public HomeContact(Guid   id,       string address, string phoneNumberCall, string phoneNumberView, string zalo,
                       string facebook, string email,   string createdBy = null)
        : base(id, createdBy)
    {
        Update(address, phoneNumberCall, phoneNumberView, zalo, facebook, email, createdBy);
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected HomeContact()
    {
    }

    /// <summary>
    ///     Địa chỉ
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    ///     Số điện thoại [Gọi điện]
    /// </summary>
    public string PhoneNumberCall { get; set; }

    /// <summary>
    ///     Số điện thoại [Hiển thị]
    /// </summary>
    public string PhoneNumberView { get; set; }

    /// <summary>
    ///     Zalo
    /// </summary>
    public string Zalo { get; set; }

    /// <summary>
    ///     Facebook
    /// </summary>
    public string Facebook { get; set; }

    /// <summary>
    ///     Email
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="address">Địa chỉ</param>
    /// <param name="phoneNumberCall">Số điện thoại [Gọi điện]</param>
    /// <param name="phoneNumberView">Số điện thoại [Hiển thị]</param>
    /// <param name="zalo">Zalo</param>
    /// <param name="facebook">Facebook</param>
    /// <param name="email">Email</param>
    /// <param name="createdBy">Người tạo</param>
    public void Update(string address, string phoneNumberCall, string phoneNumberView, string zalo, string facebook,
                       string email,   string createdBy = null)
    {
        Address         = address;
        PhoneNumberCall = phoneNumberCall;
        PhoneNumberView = phoneNumberView;
        Zalo            = zalo;
        Facebook        = facebook;
        Email           = email;
        Update(createdBy);
    }
}