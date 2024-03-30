#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsGithubManagers;
using NCore.Modals;

#endregion

namespace ITC.Domain.Interfaces.NewsManagers.NewsGithubManagers;

/// <summary>
///     Lớp interface query loại nhóm tin
/// </summary>
public interface INewsGithubQueries
{
    /// <summary>
    ///     [Phân trang] Trả về danh sách loại nhóm tin
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<NewsGithubPagingDto>> GetPaging(PagingModel model);

    /// <summary>
    ///     Xóa nhiều dữ liệu
    /// </summary>
    /// <returns></returns>
    Task<int> DeleteAsync(List<Guid> model);

    /// <summary>
    ///     [Combobox] Trả về danh sách loại nhóm tin
    /// </summary>
    /// <param name="vSearch"></param>
    /// <param name="projectId"></param>
    /// <returns></returns>
    Task<IEnumerable<ComboboxModal>> GetCombobox(string vSearch, Guid projectId);
}