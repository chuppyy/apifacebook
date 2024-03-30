#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.HomeManagers.HomeNewsGroupViewManagers;
using NCore.Modals;

#endregion

namespace ITC.Domain.Interfaces.HomeManagers.HomeNewsGroupViewManagers;

/// <summary>
///     Lớp interface query danh sách các dữ liệu bài viết hiển thị trên trang chủ
/// </summary>
public interface IHomeNewsGroupViewQueries
{
    /// <summary>
    ///     Xóa dữ liệu
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<int> DeleteAsync(List<Guid> model);

    /// <summary>
    ///     Danh sách GetHomeNewsGroupDetailByHomeNewsGroupViewId by homeNewsGroupViewId
    /// </summary>
    /// <param name="homeNewsGroupViewId">Mã homeNewsGroupViewId</param>
    /// <returns></returns>
    Task<IEnumerable<HomeNewsGroupViewDetailModel>> GetHomeNewsGroupDetailByHomeNewsGroupViewId(
        Guid homeNewsGroupViewId);

    /// <summary>
    ///     [Phân trang] Trả về danh sách các nhóm tin hiển thị trên trang chủ
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<HomeNewsGroupViewPagingDto>> GetPaging(PagingModel model);
}