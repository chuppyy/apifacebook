#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.StudyManagers.MinusWord;
using NCore.Modals;

#endregion

namespace ITC.Application.AppService.StudyManagers.MinusWord;

/// <summary>
///     Class interface service loại môn học
/// </summary>
public interface IMinusWordAppService
{
#region Methods

    /// <summary>
    ///     Thêm mới loại môn học
    /// </summary>
    /// <param name="model"></param>
    Task<bool> Add(MinusWordEventModel model);

    /// <summary>
    ///     Xóa loại môn học
    /// </summary>
    /// <param name="model"></param>
    Task<bool> Delete(DeleteModal model);

    /// <summary>
    ///     Lấy theo Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    MinusWordEventModel GetById(Guid id);

    /// <summary>
    ///     Cập nhật loại môn học
    /// </summary>
    /// <param name="model"></param>
    Task<bool> Update(MinusWordEventModel model);

    /// <summary>
    ///     [Combobox] Danh sách loại môn học
    /// </summary>
    /// <param name="vSearch">Giá trị tìm kiếm</param>
    /// <param name="groupTable">Loại dữ liệu cùng cấu trúc bảng</param>
    /// <returns></returns>
    Task<IEnumerable<ComboboxModal>> GetCombobox(string vSearch, int groupTable);

    /// <summary>
    ///     [Phân trang] Danh sách loại môn học
    /// </summary>
    /// <param name="model">Dữ liệu nhận từ FE</param>
    /// <returns></returns>
    Task<IEnumerable<MinusWordPagingDto>> GetPaging(PagingModel model);

#endregion
}