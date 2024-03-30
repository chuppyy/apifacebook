#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.Itphonui.ManagementManagers;
using NCore.Modals;

#endregion

namespace ITC.Domain.Interfaces.Itphonui.ManagementManagers;

/// <summary>
///     Lớp interface query quản lý đơn vị
/// </summary>
public interface IManagementManagerQueries
{
    /// <summary>
    ///     Xóa dữ liệu
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<int> DeleteAsync(List<Guid> model);

    /// <summary>
    ///     [TreeView] Trả về danh sách các chức năng
    /// </summary>
    /// <param name="vSearch">Giá trị tìm kiếm</param>
    /// <param name="isAll">Hiển thị tất cả</param>
    /// <param name="projectId"></param>
    /// <param name="typeId"></param>
    /// <returns></returns>
    Task<IEnumerable<TreeViewProjectModel>> GetTreeView(string vSearch, bool isAll, Guid projectId, int typeId);

    /// <summary>
    ///     Trả về danh sách đơn vị mặc định
    /// </summary>
    /// <param name="projectOld"></param>
    /// <returns></returns>
    Task<IEnumerable<ComboboxCheckModal>> GetManagementDefault(Guid projectOld);

    /// <summary>
    ///     Trả về danh sách đơn vi trong project
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="search"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<IEnumerable<ManagementViewProjectModel>> GetManagementViewProject(
        Guid projectId, string search, int pageNumber, int pageSize);
}