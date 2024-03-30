using NCore.Modals;

namespace ITC.Domain.Commands.SystemManagers.HelperManagers;

/// <summary>
///     Command cập nhật trạng thái dữ liệu
/// </summary>
public class UpdateStatusHelperCommand : HelperCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public UpdateStatusHelperCommand(UpdateStatusHelperModal model)
    {
        Id      = model.Id;
        TableId = model.TableId;
        FlagKey = model.FlagKey;
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