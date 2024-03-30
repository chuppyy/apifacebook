#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsRecruitmentManagers;
using NCore.Modals;

#endregion

namespace ITC.Domain.Interfaces.NewsManagers.NewsRecruitmentManagers;

/// <summary>
///     Lớp interface query bài viết
/// </summary>
public interface INewsRecruitmentQueries
{
    /// <summary>
    ///     [Phân trang] Trả về danh sách bài viết
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<NewsRecruitmentPagingDto>> GetPaging(PagingModel model, int type);

    /// <summary>
    ///     Xóa nhiều dữ liệu
    /// </summary>
    /// <returns></returns>
    Task<int> DeleteAsync(List<Guid> model);
}