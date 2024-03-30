using System.Collections.Generic;
using ITC.Domain.Core.ModelShare.HomeManagers.HomeNewsGroupViewManagers;

namespace ITC.Domain.Commands.HomeManagers.HomeNewsGroupViewManagers;

/// <summary>
///     Command cập nhật menu trang chủ
/// </summary>
public class UpdateHomeNewsGroupViewCommand : HomeNewsGroupViewCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="model">Dữ liệu nhận từ FE</param>
    public UpdateHomeNewsGroupViewCommand(HomeNewsGroupViewEventModel model)
    {
        Description             = model.Description;
        Name                    = model.Name;
        ParentId                = model.ParentId;
        HomeNewsGroupViewModels = model.HomeNewsGroupViewModels;
    }

#endregion

    /// <summary>
    ///     Dữ liệu liên kết với nhóm tin
    /// </summary>
    public List<HomeNewsGroupViewDetailModel> HomeNewsGroupViewModels { get; set; }

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