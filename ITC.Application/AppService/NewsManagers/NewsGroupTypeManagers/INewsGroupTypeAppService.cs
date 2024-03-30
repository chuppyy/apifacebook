#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsGroupTypeManagers;
using ITC.Domain.Models.NewsManagers;
using NCore.Modals;

#endregion

namespace ITC.Application.AppService.NewsManagers.NewsGroupTypeManagers;

/// <summary>
///     Class interface service loại nhóm tin
/// </summary>
public interface INewsGroupTypeAppService
{
#region Methods

    /// <summary>
    ///     Thêm mới loại nhóm tin
    /// </summary>
    /// <param name="model"></param>
    bool Add(NewsGroupTypeEventModel model);

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
    NewsGroupTypeEventModel GetById(Guid id);

    /// <summary>
    ///     Cập nhật loại nhóm tin
    /// </summary>
    /// <param name="model"></param>
    bool Update(NewsGroupTypeEventModel model);

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
    Task<IEnumerable<NewsGroupTypePagingDto>> GetPaging(PagingModel model);

    /// <summary>
    ///     Danh sách dữ liệu theo SecrectId
    /// </summary>
    /// <returns></returns>
    Task<NewsGroupType> GetBySecrect(Guid projectId, string secrectKey);

#endregion
}