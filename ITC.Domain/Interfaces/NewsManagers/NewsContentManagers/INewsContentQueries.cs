#region

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.HomeManager;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsContentManagers;
using NCore.Modals;

#endregion

namespace ITC.Domain.Interfaces.NewsManagers.NewsContentManagers;

/// <summary>
///     Lớp interface query bài viết
/// </summary>
public interface INewsContentQueries
{
    /// <summary>
    ///     [Phân trang] Trả về danh sách bài viết
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<NewsContentPagingDto>> GetPaging(NewsContentPagingModel model, List<Guid> newsGroupId);
    /// <summary>
    ///     [Phân trang] Trả về danh sách bài viết
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<NewsContentPagingDto>> GetPagingAuto(NewsContentPagingModel model, List<Guid> newsGroupId);
    
    /// <summary>
    ///     [Phân trang] Trả về danh sách bài viết theo ID
    /// </summary>
    /// <returns></returns>
    Task<NewsContentPagingByIdDto> GetPagingById(Guid id);

    /// <summary>
    ///     [Phân trang] Trả về danh sách bài viết trên combobox
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<NewsContentPagingComboboxDto>> GetPagingCombobox(NewsContentPagingModel model,
                                                                      List<Guid>             newsGroupId);

    /// <summary>
    ///     Xóa nhiều dữ liệu
    /// </summary>
    /// <returns></returns>
    Task<int> DeleteAsync(List<Guid> model);

    /// <summary>
    ///     [Combobox] Danh sách tác giả
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<ComboboxModalString>> NewsAuthor();

    /// <inheritdoc cref="AutoPostFaceList"/>
    Task<IEnumerable<AutoPostFaceModel>> AutoPostFaceList();
    
    /// <summary>
    /// Lưu dữ liệu domain
    /// </summary>
    /// <param name="sBuilder"></param>
    /// <returns></returns>
    Task<int> SaveDomain(StringBuilder sBuilder);
    
    /// <summary>
    /// Dữ liệu chi tiết
    /// </summary>
    /// <returns></returns>
    Task<NewsMainModel> GetDetail(string id);

    /// <summary>
    /// Danh sách bài viết theo nhóm tin
    /// </summary>
    /// <param name="groupModel">Danh sách nhóm tin cần lấy dữ liệu</param>
    /// <param name="numberOf">Danh sách nhóm tin cần lấy dữ liệu</param>
    /// <returns></returns>
    Task<List<NewsGroupMainModel>> ListContentByGroup(List<Guid> groupModel, int numberOf);
    
    /// <summary>
    /// Danh sách bài viết news life
    /// </summary>
    /// <returns></returns>
    Task<HomeNewsLifeModel> HomeNewsLifeModel(string id);
}