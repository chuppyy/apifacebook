#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.HomeManager;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsContentManagers;
using ITC.Domain.Models.NewsManagers;
using NCore.Modals;

#endregion

namespace ITC.Application.AppService.NewsManagers.NewsContentManagers;

/// <summary>
///     Class interface service bài viết
/// </summary>
public interface INewsContentAppService
{
    /// <summary>
    ///     Thêm mới bài viết
    /// </summary>
    /// <param name="model"></param>
    Task<bool> Add(NewsContentEventModel model);

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
    Task<NewsContentGetByIdModel> GetById(Guid id);

    /// <summary>
    ///     Cập nhật bài viết
    /// </summary>
    /// <param name="model"></param>
    Task<bool> Update(NewsContentEventModel model);

    /// <summary>
    ///     [Phân trang] Danh sách bài viết
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<IEnumerable<NewsContentPagingDto>> GetPaging(NewsContentPagingModel model);
    
    /// <summary>
    ///     [Phân trang] Danh sách bài viết
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<IEnumerable<NewsContentPagingDto>> GetPagingAuto(NewsContentPagingModel model);

    /// <summary>
    ///     [Combobox] Danh sách NewsContentType
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<ComboboxModalInt>> NewsContentTypeCombobox();

    /// <summary>
    ///     [Combobox] Danh sách tác giả
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<ComboboxModal>> NewsAuthor();

    /// <summary>
    ///     [Combobox] Danh sách NewsContent
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<IEnumerable<NewsContentPagingComboboxDto>> NewsContentCombobox(NewsContentPagingModel model);

    /// <summary>
    ///     Danh sách dữ liệu theo SecrectId
    /// </summary>
    /// <returns></returns>
    Task<NewsContent> GetBySecrect(Guid projectId, string secrectKey);
    
    /// <summary>
    ///     [Combobox] Copy-link
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<ComboboxModal>> CopyLink(Guid id);
    
    /// <summary>
    ///    Đọc dữ liệu từ link
    /// </summary>
    /// <returns></returns>
    Task<ReadLink> ReadLink(string id);

    /// <summary>
    /// Đăng bài
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<PostNewFaceError> PostNew(PostNewFaceEvent model);
    
    /// <summary>
    ///     Cập nhật bài viết
    /// </summary>
    /// <param name="model"></param>
    Task<bool> UpdateTimeAutoPost(NewsContentUpdateTimeAutoPostModel model);

    /// <summary>
    /// Lấy cấu hình lập lịch
    /// </summary>
    /// <param name="newsContent"></param>
    /// <returns></returns>
    Task<int> GetScheduleConfig(string newsContent);

    /// <summary>
    /// Lưu cấu hình lập lịch
    /// </summary>
    /// <returns></returns>
    Task<int> GetScheduleSave(int id);

    /// <summary>
    /// Dữ liệu chi tiết
    /// </summary>
    /// <returns></returns>
    Task<NewsMainModel> GetDetail(string id);

    /// <summary>
    /// Dữ liệu chi tiết
    /// </summary>
    /// <returns></returns>
    Task<NewsMainModel> GetDetailNew(string id);

    /// <summary>
    /// Dữ liệu chi tiết
    /// </summary>
    /// <returns></returns>
    Task<NewsMainModel> GetDetailBasic(string id);

    /// <summary>
    /// Dữ liệu chi tiết Thread
    /// </summary>
    /// <returns></returns>
    Task<NewsThreadModel> GetDetailThread(string profileId, string categoryId, int position,int top);
    /// <summary>
    /// Danh sách bài viết theo nhóm tin
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<HomeMainGroupModel>> ListContentByGroup(List<Guid> groupModel, int numberOf);

    /// <summary>
    /// Danh sách bài viết news life
    /// </summary>
    /// <returns></returns>
    Task<HomeNewsLifeModel> HomeNewsLifeModel(string id);
}