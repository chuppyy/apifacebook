#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.StudyManagers.SubjectTypeManagers;
using NCore.Modals;

#endregion

namespace ITC.Domain.Interfaces.StudyManagers.SubjectTypeManagers;

/// <summary>
///     Lớp interface query loại môn học
/// </summary>
public interface ISubjectTypeManagerQueries
{
    /// <summary>
    ///     [Phân trang] Trả về danh sách loại môn học
    /// </summary>
    /// <param name="model">Dữ liệu nhận từ FE</param>
    /// <param name="groupTable">Loại dữ liệu cùng cấu trúc bảng</param>
    /// <returns></returns>
    Task<IEnumerable<SubjectTypeManagerPagingDto>> GetPaging(PagingModel model, int groupTable);

    /// <summary>
    ///     Xóa nhiều dữ liệu
    /// </summary>
    /// <returns></returns>
    Task<int> DeleteAsync(List<Guid> model);

    /// <summary>
    ///     [Combobox] Trả về danh sách loại môn học
    /// </summary>
    /// <param name="vSearch">Giá trị tìm kiếm</param>
    /// <param name="groupTable">Loại dữ liệu cùng cấu trúc bảng</param>
    /// <param name="projectId"></param>
    /// <returns></returns>
    Task<IEnumerable<ComboboxModal>> GetCombobox(string vSearch, int groupTable, Guid projectId);
}