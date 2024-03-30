#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.SaleProductManagers.ImageLibraryDetailManager;
using ITC.Domain.Core.ModelShare.SaleProductManagers.ImageLibraryManager;
using NCore.Modals;

#endregion

namespace ITC.Application.AppService.SaleProductManagers.ImageLibraryManager;

/// <summary>
///     Class interface service slide
/// </summary>
public interface IImageLibraryManagerAppService
{
#region Methods

    /// <summary>
    ///     Thêm mới slide
    /// </summary>
    /// <param name="model"></param>
    Task<bool> Add(ImageLibraryManagerEventModel model);

    /// <summary>
    ///     Xóa slide
    /// </summary>
    /// <param name="model"></param>
    Task<bool> Delete(DeleteModal model);

    /// <summary>
    ///     Lấy theo Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    ImageLibraryManagerGetByIdModel GetById(Guid id);

    /// <summary>
    ///     Cập nhật slide
    /// </summary>
    /// <param name="model"></param>
    Task<bool> Update(ImageLibraryManagerEventModel model);

    /// <summary>
    ///     [Phân trang] Danh sách slide
    /// </summary>
    /// <param name="model">Dữ liệu nhận từ FE</param>
    /// <returns></returns>
    Task<IEnumerable<ImageLibraryManagerPagingDto>> GetPaging(PagingModel model);

    /// <summary>
    ///     [Phân trang] Danh sách slide
    /// </summary>
    /// <param name="model">Dữ liệu nhận từ FE</param>
    /// <returns></returns>
    Task<IEnumerable<ImageLibraryDetailManagerPagingDto>> GetPagingDetail(ImageLibraryDetailPagingModel model);

    /// <summary>
    ///     Danh sách loại slide hiển thị
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<ComboboxModalInt>> GetSlideTypeView();

    /// <summary>
    ///     Thêm mới slide - detail
    /// </summary>
    /// <param name="model"></param>
    Task<bool> AddDetail(ImageLibraryDetailManagerEventModel model);

    /// <summary>
    ///     Cập nhật slide - detail
    /// </summary>
    /// <param name="model"></param>
    Task<bool> UpdateDetail(ImageLibraryDetailManagerEventModel model);

    /// <summary>
    ///     Xóa slide - detail
    /// </summary>
    /// <param name="model"></param>
    Task<bool> DeleteDetail(DeleteModal model);

    /// <summary>
    ///     Lấy theo Id - detail
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    ImageLibraryDetailManagerGetByIdModel GetByIdDetail(Guid id);

    /// <summary>
    ///     Lấy dữ liệu theo SecrectKey
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="secrectKey"></param>
    /// <returns></returns>
    Task<Domain.Models.SaleProductManagers.ImageLibraryManager> GetBySecrectKey(Guid projectId, string secrectKey);

    /// <summary>
    ///     [MAIN] Danh sách slide
    /// </summary>
    /// <param name="model">Dữ liệu nhận từ FE</param>
    /// <param name="imageId"></param>
    /// <returns></returns>
    Task<IEnumerable<ImageLibraryDetailManagerPagingDto>> GetHomeDetail(ImageLibraryDetailPagingModel model,
                                                                        Guid                          imageId);

#endregion
}