#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NCore.Modals;

#endregion

namespace ITC.Domain.Interfaces.StudyManagers.SubjectManagers;

/// <summary>
///     Lớp interface query danh sách môn học
/// </summary>
public interface ISubjectManagerQueries
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
    /// <param name="subjectManagerTypeId">Mã loại môn học</param>
    /// <param name="isAll">Hiển thị tất cả</param>
    /// <param name="projectId"></param>
    /// <returns></returns>
    Task<IEnumerable<TreeViewProjectModel>> GetTreeView(string vSearch, Guid subjectManagerTypeId, bool isAll,
                                                        Guid   projectId);

    /// <summary>
    ///     [MAIN] Trả về số lượng child theo parrent
    /// </summary>
    /// <param name="projectId">Mã dự án</param>
    /// <param name="subjectId">Mã nhóm sản phẩm</param>
    /// <returns></returns>
    Task<int> CountChildFromParent(Guid projectId, Guid subjectId);
}