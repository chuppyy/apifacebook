#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.StudyManagers.MinusWord;
using NCore.Modals;

#endregion

namespace ITC.Domain.Interfaces.StudyManagers.MinusWord;

/// <summary>
///     Lớp interface query loại môn học
/// </summary>
public interface IMinusWordQueries
{
    /// <summary>
    ///     [Phân trang] Trả về danh sách loại môn học
    /// </summary>
    /// <param name="model">Dữ liệu nhận từ FE</param>
    /// <returns></returns>
    Task<IEnumerable<MinusWordPagingDto>> GetPaging(PagingModel model);

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