#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.SystemManagers.SortMenuManagers;
using NCore.Modals;

#endregion

namespace ITC.Application.AppService.SystemManagers.SortMenuManagers;

/// <summary>
///     Class interface service sắp xếp menu
/// </summary>
public interface ISortMenuManagerAppService
{
#region Methods

    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="model"></param>
    Task<bool> Update(SortMenuManagerEventModel model);

    /// <summary>
    ///     Xóa
    /// </summary>
    /// <param name="model"></param>
    Task<bool> Delete(DeleteModal model);

    /// <summary>
    ///     [Phân trang] Danh sách
    /// </summary>
    /// <param name="menuId"></param>
    /// <param name="parentId"></param>
    /// <returns></returns>
    Task<IEnumerable<SortMenuManagerDto>> GetSortMenu(Guid menuId, Guid parentId);

#endregion
}