#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsSeoKeyWordManagers;
using NCore.Modals;

#endregion

namespace ITC.Application.AppService.NewsManagers.NewsSeoKeyWordManagers;

/// <summary>
///     Class interface service từ khóa SEO
/// </summary>
public interface INewsSeoKeyWordAppService
{
#region Methods

    /// <summary>
    ///     Thêm mới từ khóa SEO
    /// </summary>
    /// <param name="model"></param>
    bool Add(NewsSeoKeyWordEventModel model);

    /// <summary>
    ///     Xóa từ khóa SEO
    /// </summary>
    /// <param name="model"></param>
    bool Delete(DeleteModal model);

    /// <summary>
    ///     Lấy theo Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    NewsSeoKeyWordEventModel GetById(Guid id);

    /// <summary>
    ///     Cập nhật từ khóa SEO
    /// </summary>
    /// <param name="model"></param>
    bool Update(NewsSeoKeyWordEventModel model);

    /// <summary>
    ///     [Combobox] Danh sách từ khóa SEO
    /// </summary>
    /// <param name="vSearch"></param>
    /// <returns></returns>
    Task<IEnumerable<ComboboxModal>> GetCombobox(string vSearch);

    /// <summary>
    ///     [Phân trang] Danh sách từ khóa SEO
    /// </summary>
    /// <param name="model">Dữ liệu nhận từ FE</param>
    /// <returns></returns>
    Task<IEnumerable<NewsSeoKeyWordPagingDto>> GetPaging(PagingModel model);

#endregion
}