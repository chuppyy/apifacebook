namespace ITC.Domain.Commands.SystemManagers.ServerFileManagers;

/// <summary>
///     Command call đệ quy server-file
/// </summary>
public class CallDeQuyServerFileCommand : ServerFileCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public CallDeQuyServerFileCommand()
    {
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