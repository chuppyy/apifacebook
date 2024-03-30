#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsGithubManagers;
using NCore.Modals;

#endregion

namespace ITC.Application.AppService.NewsManagers.NewsGithubManagers;

/// <summary>
///     Class interface service loại nhóm tin
/// </summary>
public interface INewsGithubAppService
{
#region Methods

    /// <summary>
    ///     Thêm mới loại nhóm tin
    /// </summary>
    /// <param name="model"></param>
    bool Add(NewsGithubEventModel model);

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
    NewsGithubEventModel GetById(Guid id);

    /// <summary>
    ///     Cập nhật loại nhóm tin
    /// </summary>
    /// <param name="model"></param>
    bool Update(NewsGithubEventModel model);

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
    Task<IEnumerable<NewsGithubPagingDto>> GetPaging(PagingModel model);

#endregion
}