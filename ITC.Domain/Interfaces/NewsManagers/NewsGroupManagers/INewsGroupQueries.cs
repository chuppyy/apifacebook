#region

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsGroupTypeManagers;
using NCore.Modals;

#endregion

namespace ITC.Domain.Interfaces.NewsManagers.NewsGroupManagers;

/// <summary>
///     Lớp interface query danh sách nhóm tin
/// </summary>
public interface INewsGroupQueries
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
    /// <param name="newsGroupTypeId">Mã loại nhóm tin</param>
    /// <param name="isAll">Hiển thị tất cả</param>
    /// <param name="projectId"></param>
    /// <returns></returns>
    Task<IEnumerable<TreeViewProjectModel>> GetTreeView(string vSearch, Guid newsGroupTypeId, bool isAll,
                                                        Guid   projectId);


    /// <summary>
    /// Lấy dữ liệu Vercel
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<ListVercelDto>> ListDomainVercel();
    
    /// <summary>
    /// Lưu dữ liệu domain
    /// </summary>
    /// <param name="sBuilder"></param>
    /// <returns></returns>
    Task<int> SaveDomain(StringBuilder sBuilder);
}