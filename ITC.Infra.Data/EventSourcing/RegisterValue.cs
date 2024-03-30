namespace ITC.Infra.Data.EventSourcing;

/// <summary>
///     Lớp lưu giữ thông tin của giá trị
/// </summary>
public class RegisterValue
{
#region Constructors

    /// <summary>
    ///     Hàm khởi tạo
    /// </summary>
    /// <param name="displayName"></param>
    /// <param name="message"></param>
    internal RegisterValue(string displayName, string message)
    {
        DisplayName = displayName;
        Message     = message;
    }

#endregion

#region Properties

    /// <summary>
    ///     Tên hiển thị
    /// </summary>
    public string DisplayName { get; }

    /// <summary>
    ///     Tin nhắn hiển thị nghĩa
    /// </summary>
    public string Message { get; }

#endregion
}