#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.HomeManagers.HomeMenuManagers;
using NCore.Modals;

#endregion

namespace ITC.Domain.Interfaces.HomeManagers.HomeMenuManagers;

/// <summary>
///     Lớp interface query danh sách menu trang chủ
/// </summary>
public interface IHomeMenuQueries
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
    /// <param name="projectId">Mã dự án</param>
    /// <returns></returns>
    Task<IEnumerable<TreeViewProjectModel>> GetTreeView(string vSearch, Guid projectId);

    /// <summary>
    ///     Danh sách newsGroup by homeMenu
    /// </summary>
    /// <param name="homeMenuId">Mã homeMenuId</param>
    /// <returns></returns>
    Task<IEnumerable<HomeMenuNewsGroupModel>> GetHomeMenuNewsGroupByHomeMenuId(Guid homeMenuId);
}