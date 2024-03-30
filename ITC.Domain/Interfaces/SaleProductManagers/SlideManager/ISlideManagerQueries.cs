#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.SaleProductManagers.SlideManager;
using NCore.Modals;

#endregion

namespace ITC.Domain.Interfaces.SaleProductManagers.SlideManager;

/// <summary>
///     Lớp interface query slide
/// </summary>
public interface ISlideManagerQueries
{
    /// <summary>
    ///     [Phân trang] Trả về danh sách slide
    /// </summary>
    /// <param name="model">Dữ liệu nhận từ FE</param>
    /// <returns></returns>
    Task<IEnumerable<SlideManagerPagingDto>> GetPaging(PagingModel model);

    /// <summary>
    ///     Xóa nhiều dữ liệu
    /// </summary>
    /// <returns></returns>
    Task<int> DeleteAsync(List<Guid> model);
}