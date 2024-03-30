#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.SaleProductManagers.AboutManager;
using NCore.Modals;

#endregion

namespace ITC.Domain.Interfaces.SaleProductManagers.AboutManager;

/// <summary>
///     Lớp interface query giới thiệu
/// </summary>
public interface IAboutManagerQueries
{
    /// <summary>
    ///     [Phân trang] Trả về danh sách
    /// </summary>
    /// <param name="model">Dữ liệu nhận từ FE</param>
    /// <param name="typeId"></param>
    /// <returns></returns>
    Task<IEnumerable<AboutManagerPagingDto>> GetPaging(PagingModel model, int typeId);

    /// <summary>
    ///     Xóa nhiều dữ liệu
    /// </summary>
    /// <returns></returns>
    Task<int> DeleteAsync(List<Guid> model);

    /// <summary>
    ///     Xóa nhiều dữ liệu - Attack
    /// </summary>
    /// <returns></returns>
    Task<int> DeleteAttackAsync(List<Guid> model);

    /// <summary>
    ///     [Combobox] Danh sách giới thiệu combobox
    /// </summary>
    /// <param name="projectId"></param>
    /// <returns></returns>
    Task<IEnumerable<ComboboxAboutManager>> GetComboboxAboutManager(Guid projectId);

    /// <summary>
    ///     [Phân trang] Danh sách giới thiệu - hình ảnh
    /// </summary>
    /// <param name="id">Mã giới thiệu</param>
    /// <returns></returns>
    Task<IEnumerable<AboutAttackManagerPagingDto>> GetPagingAttack(Guid id);

    /// <summary>
    ///     Danh sách giới thiệu - hình ảnh
    /// </summary>
    /// <param name="id">Mã giới thiệu</param>
    /// <param name="managementId"></param>
    /// <returns></returns>
    Task<IEnumerable<ImageAttackModel>> GetImageAttackModel(Guid id, Guid managementId);
}