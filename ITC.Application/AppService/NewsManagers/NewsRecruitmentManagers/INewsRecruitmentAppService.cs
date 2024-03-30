#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsRecruitmentManagers;
using ITC.Domain.Models.NewsManagers;
using NCore.Modals;

#endregion

namespace ITC.Application.AppService.NewsManagers.NewsRecruitmentManagers;

/// <summary>
///     Class interface service bài viết
/// </summary>
public interface INewsRecruitmentAppService
{
    /// <summary>
    ///     Thêm mới bài viết
    /// </summary>
    /// <param name="model"></param>
    Task<bool> Add(NewsRecruitmentEventModel model);

    /// <summary>
    ///     Xóa bài viết
    /// </summary>
    /// <param name="model"></param>
    bool Delete(DeleteModal model);

    /// <summary>
    ///     Lấy theo Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<NewsRecruitmentGetByIdModel> GetById(Guid id);

    /// <summary>
    ///     Cập nhật bài viết
    /// </summary>
    /// <param name="model"></param>
    Task<bool> Update(NewsRecruitmentEventModel model);

    /// <summary>
    ///     [Phân trang] Danh sách bài viết
    /// </summary>
    /// <param name="model"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    Task<IEnumerable<NewsRecruitmentPagingDto>> GetPaging(PagingModel model, int type);

    /// <summary>
    ///     Danh sách dữ liệu theo SecrectId
    /// </summary>
    /// <returns></returns>
    Task<NewsRecruitment> GetBySecrect(Guid projectId, string secrectKey);
}