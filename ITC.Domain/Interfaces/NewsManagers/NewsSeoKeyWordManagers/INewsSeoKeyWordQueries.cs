#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsSeoKeyWordManagers;
using NCore.Modals;

#endregion

namespace ITC.Domain.Interfaces.NewsManagers.NewsSeoKeyWordManagers;

/// <summary>
///     Lớp interface query từ khóa SEO
/// </summary>
public interface INewsSeoKeyWordQueries
{
    /// <summary>
    ///     [Phân trang] Trả về danh sách từ khóa SEO
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<NewsSeoKeyWordPagingDto>> GetPaging(PagingModel model, Guid projectId);

    /// <summary>
    ///     Xóa nhiều dữ liệu
    /// </summary>
    /// <returns></returns>
    Task<int> DeleteAsync(List<Guid> model);

    /// <summary>
    ///     [Combobox] Trả về danh sách từ khóa SEO
    /// </summary>
    /// <param name="vSearch"></param>
    /// <param name="projectId"></param>
    /// <returns></returns>
    Task<IEnumerable<ComboboxModal>> GetCombobox(string vSearch, Guid projectId);
}