#region

using System;
using System.Threading.Tasks;

#endregion

namespace ITC.Domain.Interfaces.HomeManagers.HomeNewsGroupDetailManagers;

/// <summary>
///     Lớp interface query danh sách các bài viết hiển thị trên trang chủ liên kết với nhóm tin
/// </summary>
public interface IHomeNewsGroupViewDetailQueries
{
    /// <summary>
    ///     Xóa dữ liệu
    /// </summary>
    /// <returns></returns>
    Task<int> DeleteAsync(Guid homeMenuId);

    /// <summary>
    ///     Xóa dữ liệu history
    /// </summary>
    /// <param name="homeMenuId"></param>
    /// <param name="history"></param>
    /// <returns></returns>
    Task<int> DeleteHistory(Guid homeMenuId, int history);
}