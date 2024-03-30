using System.Collections.Generic;
using ITC.Domain.Core.ModelShare.HomeManagers.HomeNewsGroupViewManagers;

namespace ITC.Domain.Commands.HomeManagers.HomeNewsGroupViewManagers;

/// <summary>
///     Command thêm menu trang chủ
/// </summary>
public class AddHomeNewsGroupViewCommand : HomeNewsGroupViewCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="model">Dữ liệu nhận từ FE</param>
    public AddHomeNewsGroupViewCommand(HomeNewsGroupViewEventModel model)
    {
        Description             = model.Description;
        Name                    = model.Name;
        ParentId                = model.ParentId;
        Url                     = model.Url;
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