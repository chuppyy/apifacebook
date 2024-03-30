using ITC.Domain.Core.ModelShare.SystemManagers.HelperManagers;

namespace ITC.Domain.Commands.SystemManagers.HelperManagers;

/// <summary>
///     Command kiểm tra thời gian
/// </summary>
public class CheckTimeHelperCommand : HelperCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public CheckTimeHelperCommand(CheckTimeModel model)
    {
        Type = model.TypeId;
    }

#endregion

    public int Type { get; set; }

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