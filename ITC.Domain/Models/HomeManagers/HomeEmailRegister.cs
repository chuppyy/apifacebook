using System;
using ITC.Domain.Core.Models;

namespace ITC.Domain.Models.HomeManagers;

/// <summary>
///     Email khách hàng đăng ký nhận thông tin
/// </summary>
public class HomeEmailRegister : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public HomeEmailRegister(Guid id, string email, DateTime sendDateTime, string createdBy = null)
        : base(id, createdBy)
    {
        Email        = email;
        SendDateTime = sendDateTime;
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected HomeEmailRegister()
    {
    }

    /// <summary>
    ///     Email
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    ///     Thời gian đăng ký
    /// </summary>
    public DateTime SendDateTime { get; set; }
}