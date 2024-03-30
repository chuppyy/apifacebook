using System.Collections.Generic;
using NCore.Modals;

namespace ITC.Domain.Commands.NewsManagers.MinusWord;

/// <summary>
///     Command Cập nhật vị trí môn học
/// </summary>
public class UpdatePositionMinusWordCommand : MinusWordCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="model">Dữ liệu nhận từ FE</param>
    /// <param name="createBy">Người tạo</param>
    public UpdatePositionMinusWordCommand(UpdatePositionModal model, string createBy)
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