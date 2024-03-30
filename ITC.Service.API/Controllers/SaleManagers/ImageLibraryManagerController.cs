#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITC.Application.AppService.SaleProductManagers.ImageLibraryManager;
using ITC.Application.Helpers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.SaleProductManagers.ImageLibraryDetailManager;
using ITC.Domain.Core.ModelShare.SaleProductManagers.ImageLibraryManager;
using ITC.Domain.Core.Notifications;
using ITC.Infra.CrossCutting.Identity.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NCore.Modals;
using NCore.Responses;
using NCore.Systems;

#endregion

namespace ITC.Service.API.Controllers.SaleManagers;

/// <summary>
///     Thư viện hình ảnh
/// </summary>
[Route("[controller]")]
[ApiController]
[Authorize]
public class ImageLibraryManagerController : ApiController
{
#region Fields

    private readonly IImageLibraryManagerAppService _imageLibraryManagerAppService;

#endregion

#region Constructors

    /// <summary>
    ///     Hàm dựng
    /// </summary>
    /// <param name="imageLibraryManagerAppService"></param>
    /// <param name="notifications"></param>
    /// <param name="mediator"></param>
    public ImageLibraryManagerController(IImageLibraryManagerAppService           imageLibraryManagerAppService,
                                         INotificationHandler<DomainNotification> notifications,
                                         IMediatorHandler                         mediator) :
        base(notifications, mediator)
    {
        _imageLibraryManagerAppService = imageLibraryManagerAppService;
    }

#endregion

#region Methods

    /// <summary>
    ///     Thêm mới slide
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("create")]
    [CustomAuthorize(ModuleIdentity.ImageLibraryManager, TypeAudit.Add)]
    public async Task<IActionResult> Add([FromBody] ImageLibraryManagerEventModel model)
    {
        if (ModelState.IsValid) return NResponseCommand(await _imageLibraryManagerAppService.Add(model), model);
        NotifyModelStateErrors();
        return NResponseCommand(false, model);
    }

    /// <summary>
    ///     Sửa slide
    /// </summary>
    /// <param name="model">Model menu</param>
    /// <returns></returns>
    [HttpPut("update")]
    [CustomAuthorize(ModuleIdentity.ImageLibraryManager, TypeAudit.Edit)]
    public async Task<IActionResult> Edit([FromBody] ImageLibraryManagerEventModel model)
    {
        if (ModelState.IsValid) return NResponseCommand(await _imageLibraryManagerAppService.Update(model), model);
        NotifyModelStateErrors();
        return NResponseCommand(false, model);
    }

    /// <summary>
    ///     Xóa slide
    /// </summary>
    /// <param name="model">danh sách Id xóa</param>
    /// <returns></returns>
    [HttpPost("delete")]
    [CustomAuthorize(ModuleIdentity.ImageLibraryManager, TypeAudit.Delete)]
    public async Task<IActionResult> Delete([FromBody] DeleteModal model)
    {
        return NResponseCommand(await _imageLibraryManagerAppService.Delete(model), model);
    }

    /// <summary>
    ///     Lấy slide theo Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("get-by-id/{id:guid}")]
    [CustomAuthorize(ModuleIdentity.ImageLibraryManager, TypeAudit.View)]
    public IActionResult GetById(Guid id)
    {
        return NResponseCommand(null, _imageLibraryManagerAppService.GetById(id));
    }

    /// <summary>
    ///     [Phân trang] Danh sách slide
    /// </summary>
    /// <returns></returns>
    [CustomAuthorize(ModuleIdentity.ImageLibraryManager, TypeAudit.View)]
    [HttpGet("get-paging")]
    public async Task<JsonResponse<Pagination<ImageLibraryManagerPagingDto>>> GetPaging(
        [FromQuery] PagingModel model)
    {
        return await Task.Run(() =>
        {
            model.ModuleIdentity = ModuleIdentity.ImageLibraryManager;
            var lData = _imageLibraryManagerAppService.GetPaging(model).Result.ToList();
            return new OkResponse<Pagination<ImageLibraryManagerPagingDto>>
            ("",
             new Pagination<ImageLibraryManagerPagingDto>
             {
                 PageLists = lData,
                 TotalRecord = lData.Count > 0
                                   ? lData[0].TotalRecord
                                   : 0
             });
        });
    }

    /// <summary>
    ///     [Phân trang] Danh sách slide - chi tiết
    /// </summary>
    /// <returns></returns>
    [CustomAuthorize(ModuleIdentity.ImageLibraryManager, TypeAudit.View)]
    [HttpGet("get-paging-detail")]
    public async Task<JsonResponse<Pagination<ImageLibraryDetailManagerPagingDto>>>
        GetPagingDetail([FromQuery] ImageLibraryDetailPagingModel model)
    {
        return await Task.Run(() =>
        {
            model.ModuleIdentity = ModuleIdentity.ImageLibraryDetailManager;
            var lData = _imageLibraryManagerAppService.GetPagingDetail(model).Result.ToList();
            return new OkResponse<Pagination<ImageLibraryDetailManagerPagingDto>>
            ("",
             new Pagination<ImageLibraryDetailManagerPagingDto>
             {
                 PageLists = lData,
                 TotalRecord = lData.Count > 0
                                   ? lData[0].TotalRecord
                                   : 0
             });
        });
    }

    /// <summary>
    ///     Combobox
    /// </summary>
    /// <returns></returns>
    [HttpGet("get-slide-type-view-combobox")]
    public async Task<JsonResponse<IEnumerable<ComboboxModalInt>>> GetComboboxSlideTypeView()
    {
        return
            new OkResponse<IEnumerable<ComboboxModalInt>>("", await _imageLibraryManagerAppService.GetSlideTypeView());
    }

    /// <summary>
    ///     Thêm mới slide
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("create-detail")]
    [CustomAuthorize(ModuleIdentity.ImageLibraryManager, TypeAudit.Add)]
    public async Task<IActionResult> Add([FromBody] ImageLibraryDetailManagerEventModel model)
    {
        if (ModelState.IsValid) return NResponseCommand(await _imageLibraryManagerAppService.AddDetail(model), model);
        NotifyModelStateErrors();
        return NResponseCommand(false, model);
    }

    /// <summary>
    ///     Sửa slide
    /// </summary>
    /// <param name="model">Model menu</param>
    /// <returns></returns>
    [HttpPut("update-detail")]
    [CustomAuthorize(ModuleIdentity.ImageLibraryManager, TypeAudit.Edit)]
    public async Task<IActionResult> EditDetail([FromBody] ImageLibraryDetailManagerEventModel model)
    {
        if (ModelState.IsValid)
            return NResponseCommand(await _imageLibraryManagerAppService.UpdateDetail(model), model);
        NotifyModelStateErrors();
        return NResponseCommand(false, model);
    }

    /// <summary>
    ///     Xóa slide
    /// </summary>
    /// <param name="model">danh sách Id xóa</param>
    /// <returns></returns>
    [HttpPost("delete-detail")]
    [CustomAuthorize(ModuleIdentity.ImageLibraryManager, TypeAudit.Delete)]
    public async Task<IActionResult> DeleteDetail([FromBody] DeleteModal model)
    {
        return NResponseCommand(await _imageLibraryManagerAppService.DeleteDetail(model), model);
    }

#endregion
}