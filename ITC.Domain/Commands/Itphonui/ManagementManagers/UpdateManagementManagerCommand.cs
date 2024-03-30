using ITC.Domain.Core.ModelShare.Itphonui.ManagementManagers;

namespace ITC.Domain.Commands.Itphonui.ManagementManagers;

/// <summary>
///     Command cập nhật quản lý đơn vị
/// </summary>
public class UpdateManagementManagerCommand : ManagementManagerCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="model">Dữ liệu nhận từ FE</param>
    public UpdateManagementManagerCommand(ManagementManagerEventModel model)
    {
        Id                 = model.Id;
        Description        = model.Description;
        Name               = model.Name;
        ParentId           = model.ParentId;
        Symbol             = model.Symbol;
        AccountDefault     = model.AccountDefault;
        LevelCompetitionId = model.LevelCompetitionId;
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