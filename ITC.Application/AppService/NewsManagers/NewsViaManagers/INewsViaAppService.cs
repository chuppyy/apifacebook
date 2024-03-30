#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.StudyManagers.NewsVia;
using NCore.Modals;

#endregion

namespace ITC.Application.AppService.NewsManagers.NewsViaManagers;

/// <summary>
///     Class interface service loại nhóm tin
/// </summary>
public interface INewsViaAppService
{
#region Methods

    /// <summary>
    ///     Thêm mới loại nhóm tin
    /// </summary>
    /// <param name="model"></param>
    bool Add(NewsViaEventModel model);

    /// <summary>
    ///     Xóa loại nhóm tin
    /// </summary>
    /// <param name="model"></param>
    bool Delete(DeleteModal model);

    /// <summary>
    ///     Lấy theo Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    NewsViaEventModel GetById(Guid id);

    /// <summary>
    ///     Lấy theo mã idQc
    /// </summary>
    /// <param name="idQc">Mã IdQc</param>
    /// <returns></returns>
    NewsViaEventModel GetByIdQc(string idQc);

    /// <summary>
    ///     Cập nhật loại nhóm tin
    /// </summary>
    /// <param name="model"></param>
    bool Update(NewsViaEventModel model);

    /// <summary>
    ///     [Combobox] Danh sách loại nhóm tin
    /// </summary>
    /// <param name="vSearch"></param>
    /// <returns></returns>
    Task<IEnumerable<ComboboxModal>> GetCombobox(string vSearch);

    /// <summary>
    ///     [Phân trang] Danh sách loại nhóm tin
    /// </summary>
    /// <param name="model">Dữ liệu nhận từ FE</param>
    /// <returns></returns>
    Task<IEnumerable<NewsViaPagingDto>> GetPaging(PagingModel model);

#endregion
}