#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsGroupManagers;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsGroupTypeManagers;
using ITC.Domain.Core.ModelShare.PublishManagers;
using ITC.Domain.Models.NewsManagers;
using NCore.Modals;

#endregion

namespace ITC.Application.AppService.NewsManagers.NewsGroupManagers;

/// <summary>
///     Class interface service NewsGroup
/// </summary>
public interface INewsGroupAppService
{
    /// <summary>
    ///     Thêm mới nhóm tin
    /// </summary>
    /// <param name="model"></param>
    Task<bool> Add(NewsGroupEventModel model);

    /// <summary>
    ///     Xóa nhóm tin
    /// </summary>
    /// <param name="model"></param>
    Task<bool> Delete(DeleteModal model);

    /// <summary>
    ///     Lấy theo Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    NewsGroupEventModel GetById(Guid id);

    /// <summary>
    ///     Cập nhật nhóm tin
    /// </summary>
    /// <param name="model"></param>
    Task<bool> Update(NewsGroupEventModel model);

    /// <summary>
    ///     [TreeView] Trả về danh sách các chức năng
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<IEnumerable<TreeViewProjectModel>> GetTreeView(TreeViewPagingModel model);

    /// <summary>
    ///     Cập nhật vị trí nhóm tin
    /// </summary>
    /// <param name="model"></param>
    Task<bool> UpdateLocation(UpdatePositionModal model);

    /// <summary>
    ///     Trả về danh sách tất cả mã cha con liên quan nhau
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<List<Guid>> GetListIdFromListId(List<Guid> id);

    /// <summary>
    ///     Danh sách dữ liệu theo SecrectId
    /// </summary>
    /// <returns></returns>
    Task<NewsGroup> GetBySecrect(Guid projectId, string secrectKey);

    /// <summary>
    ///     Danh sách dữ liệu vercel
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<ListVercelDto>> ListDomainVercel();

    /// <summary>
    ///     Danh sách dữ liệu vercel
    /// </summary>
    /// <returns></returns>
    Task<string> ChangeDomainVercel();
}