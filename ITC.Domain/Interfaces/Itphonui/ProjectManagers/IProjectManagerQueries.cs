#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.Itphonui.ProjectManagers;
using NCore.Modals;

#endregion

namespace ITC.Domain.Interfaces.Itphonui.ProjectManagers;

/// <summary>
///     Lớp interface query quản lý dự án
/// </summary>
public interface IProjectManagerQueries
{
    /// <summary>
    ///     Xóa nhiều dữ liệu
    /// </summary>
    /// <returns></returns>
    Task<int> DeleteAsync(List<Guid> model);

    /// <summary>
    ///     [Phân trang] Danh sách dự án
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<IEnumerable<ProjectManagerPagingModel>> GetPaging(PagingModel model);

    /// <summary>
    ///     [Combobox] Trả về danh sách
    /// </summary>
    /// <param name="vSearch"></param>
    /// <returns></returns>
    Task<IEnumerable<ComboboxModal>> GetCombobox(string vSearch);
}