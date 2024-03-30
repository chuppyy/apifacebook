using System.Collections.Generic;
using NCore.Modals;

namespace ITC.Domain.Commands.HomeManagers.HomeNewsGroupViewManagers;

/// <summary>
///     Command Cập nhật vị trí menu trang chủ
/// </summary>
public class UpdatePositionHomeNewsGroupViewCommand : HomeNewsGroupViewCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="model">Dữ liệu nhận từ FE</param>
    public UpdatePositionHomeNewsGroupViewCommand(UpdatePositionModal model)
    {
        LocationModals = model.Modals;
    }

#endregion

    /// <summary>
    ///     Danh sách các vị trí cần cập nhật
    /// </summary>
    public List<LocationModal> LocationModals { get; set; }

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