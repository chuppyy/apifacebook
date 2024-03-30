using System.Collections.Generic;
using ITC.Domain.Core.ModelShare.HomeManagers.HomeMenuManagers;

namespace ITC.Domain.Commands.HomeManagers.HomeMenuManagers;

/// <summary>
///     Command cập nhật menu trang chủ
/// </summary>
public class UpdateHomeMenuCommand : HomeMenuCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="model">Dữ liệu nhận từ FE</param>
    public UpdateHomeMenuCommand(HomeMenuEventModel model)
    {
        Description             = model.Description;
        Name                    = model.Name;
        ParentId                = model.ParentId;
        IsViewHomePage          = model.IsViewHomePage;
        HomeMenuNewsGroupModels = model.HomeMenuNewsGroupModels;
    }

#endregion

    /// <summary>
    ///     Dữ liệu liên kết với nhóm tin
    /// </summary>
    public List<HomeMenuNewsGroupModel> HomeMenuNewsGroupModels { get; set; }

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