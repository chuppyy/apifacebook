using ITC.Domain.Core.ModelShare.SystemManagers.RegisterEmailManagers;

namespace ITC.Domain.Commands.SystemManagers.RegisterEmailManagers;

/// <summary>
///     Command đăng ký email từ trang chủ
/// </summary>
public class RegRegisterEmailCommand : RegisterEmailCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public RegRegisterEmailCommand(RegisterEmailEventModel model)
    {
        Email = model.Email;
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