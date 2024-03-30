namespace ITC.Service.API.Models;

/// <summary>
///     Model gửi email xác nhận tài khoản
/// </summary>
public class ConfirmEmailModel
{
#region Fields

    /// <summary>
    /// </summary>
    public string Key = "calbackUrl";

#endregion

#region Constructors

    /// <summary>
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="url"></param>
    /// <param name="management"></param>
    /// <param name="email"></param>
    /// <param name="phoneNumber"></param>
    public ConfirmEmailModel(string userName, string url, string management, string email, string phoneNumber)
    {
        UserName    = userName;
        Url         = url;
        Management  = management;
        Email       = email;
        PhoneNumber = phoneNumber;
    }

#endregion

#region Properties

    /// <summary>
    ///     Địa chỉ email gửi đến
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    ///     Đơn vị gửi
    /// </summary>
    public string Management { get; set; }

    /// <summary>
    ///     Số điện thoại
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    ///     Đường dẫn kích hoạt
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    ///     Tài khoản
    /// </summary>
    public string UserName { get; set; }

#endregion
}