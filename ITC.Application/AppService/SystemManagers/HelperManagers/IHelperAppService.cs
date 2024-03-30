#region

using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.SystemManagers.HelperManagers;
using NCore.Modals;

#endregion

namespace ITC.Application.AppService.SystemManagers.HelperManagers;

/// <summary>
///     Class interface service Helper
/// </summary>
public interface IHelperAppService
{
    /// <summary>
    ///     [Combobox] Trạng thái hiển thị danh sách file đính kèm
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<ComboboxModalInt>> GetAttackViewCombobox();

    /// <summary>
    ///     Cập nhật nhóm tin
    /// </summary>
    /// <param name="model"></param>
    Task<bool> UpdateStatus(UpdateStatusHelperModal model);

    /// <summary>
    ///     Kiểm tra thời gian
    /// </summary>
    /// <param name="model"></param>
    Task<bool> CheckTime(CheckTimeModel model);
}