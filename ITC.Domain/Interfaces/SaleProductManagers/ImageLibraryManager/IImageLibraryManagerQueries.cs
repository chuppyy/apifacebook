#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.SaleProductManagers.ImageLibraryDetailManager;
using ITC.Domain.Core.ModelShare.SaleProductManagers.ImageLibraryManager;
using NCore.Modals;

#endregion

namespace ITC.Domain.Interfaces.SaleProductManagers.ImageLibraryManager;

/// <summary>
///     Lớp interface query slide
/// </summary>
public interface IImageLibraryManagerQueries
{
    /// <summary>
    ///     [Phân trang] Trả về danh sách slide
    /// </summary>
    /// <param name="model">Dữ liệu nhận từ FE</param>
    /// <returns></returns>
    Task<IEnumerable<ImageLibraryManagerPagingDto>> GetPaging(PagingModel model);

    /// <summary>
    ///     [Phân trang] Trả về danh sách chi tieets
    /// </summary>
    /// <param name="model">Dữ liệu nhận từ FE</param>
    /// <returns></returns>
    Task<IEnumerable<ImageLibraryDetailManagerPagingDto>> GetPagingDetail(ImageLibraryDetailPagingModel model);

    /// <summary>
    ///     Xóa nhiều dữ liệu
    /// </summary>
    /// <returns></returns>
    Task<int> DeleteAsync(List<Guid> model, int flag);
}