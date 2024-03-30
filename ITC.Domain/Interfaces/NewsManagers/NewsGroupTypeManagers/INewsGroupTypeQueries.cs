#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsGroupTypeManagers;
using NCore.Modals;

#endregion

namespace ITC.Domain.Interfaces.NewsManagers.NewsGroupTypeManagers;

/// <summary>
///     Lớp interface query loại nhóm tin
/// </summary>
public interface INewsGroupTypeQueries
{
    /// <summary>
    ///     [Phân trang] Trả về danh sách loại nhóm tin
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<NewsGroupTypePagingDto>> GetPaging(PagingModel model);

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