using System;
using ITC.Domain.Core.Models;

namespace ITC.Domain.Models.HomeManagers;

/// <summary>
///     Khách hàng liên hệ
/// </summary>
public class HomeCustomerContact : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public HomeCustomerContact(Guid     id,           string name, string email, string phoneNumber, string content,
                               DateTime sendDateTime, string createdBy = null)
        : base(id, createdBy)
    {
        Name         = name;
        Email        = email;
        PhoneNumber  = phoneNumber;
        Content      = content;
        SendDateTime = sendDateTime;
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected HomeCustomerContact()
    {
    }

    /// <summary>
    ///     Họ và tên
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Email
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    ///     Số điện thoại
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    ///     Nội dung
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    ///     Thời gian gửi
    /// </summary>
    public DateTime SendDateTime { get; set; }
}