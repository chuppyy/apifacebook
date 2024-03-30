#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsAttackManagers;

#endregion

namespace ITC.Domain.Interfaces.NewsManagers.NewsAttackManagers;

/// <summary>
///     Lớp interface query bài viết đính kèm
/// </summary>
public interface INewsAttackQueries
{
    /// <summary>
    ///     Xóa dữ liệu
    /// </summary>
    /// <returns></returns>
    Task<int> DeleteAsync(Guid newsContentId);

    /// <summary>
    ///     Danh sách file đính kèm theo NewsContentId
    /// </summary>
    /// <param name="newsContentId">Mã bài viết</param>
    /// <returns></returns>
    Task<IEnumerable<NewsAttackPagingDto>> GetPaging(Guid newsContentId);
}