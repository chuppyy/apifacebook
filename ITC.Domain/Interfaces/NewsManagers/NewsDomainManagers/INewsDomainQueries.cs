#region

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsDomainManagers;
using NCore.Modals;

#endregion

namespace ITC.Domain.Interfaces.NewsManagers.NewsDomainManagers;

/// <summary>
///     Lớp interface query loại nhóm tin
/// </summary>
public interface INewsDomainQueries
{
    /// <summary>
    ///     [Phân trang] Trả về danh sách loại nhóm tin
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<NewsDomainPagingDto>> GetPaging(PagingModel model);

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

    /// <summary>
    /// Trả về 1 dữ liệu domain
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<ComboboxModal>> GetFirstDomain();

    /// <summary>
    /// Lưu dữ liệu domain
    /// </summary>
    /// <param name="sBuilder"></param>
    /// <returns></returns>
    Task<int> SaveDomain(StringBuilder sBuilder);
}