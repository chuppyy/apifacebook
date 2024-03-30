#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Application.AppService.NewsManagers.NewsGroupManagers;
using ITC.Application.Helpers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsGroupManagers;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsGroupTypeManagers;
using ITC.Domain.Core.ModelShare.PublishManagers;
using ITC.Domain.Core.Notifications;
using ITC.Infra.CrossCutting.Identity.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NCore.Modals;
using NCore.Responses;
using NCore.Systems;

#endregion

namespace ITC.Service.API.Controllers.NewsManagers;

/// <summary>
///     Nhóm tin
/// </summary>
[Route("[controller]")]
[ApiController]
[Authorize]
public class NewsGroupController : ApiController
{
#region Fields

    private readonly INewsGroupAppService _newsGroupAppService;

#endregion

#region Constructors

    /// <summary>
    ///     Hàm dựng
    /// </summary>
    /// <param name="newsGroupAppService"></param>
    /// <param name="notifications"></param>
    /// <param name="mediator"></param>
    public NewsGroupController(INewsGroupAppService                     newsGroupAppService,
                               INotificationHandler<DomainNotification> notifications,
                               IMediatorHandler                         mediator) :
        base(notifications, mediator)
    {
        _newsGroupAppService = newsGroupAppService;
    }

#endregion

#region Methods

    /// <summary>
    ///     Thêm mới Nhóm tin
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("create")]
    [CustomAuthorize(ModuleIdentity.NewsGroup, TypeAudit.Add)]
    public async Task<IActionResult> Add([FromBody] NewsGroupEventModel model)
    {
        model.AmountPosted = 0;
        if (ModelState.IsValid) return NResponseCommand(await _newsGroupAppService.Add(model), model);
        NotifyModelStateErrors();
        return NResponseCommand(false, model);

    }

    /// <summary>
    ///     Sửa Nhóm tin
    /// </summary>
    /// <param name="model">Model menu</param>
    /// <returns></returns>
    [HttpPut("update")]
    [CustomAuthorize(ModuleIdentity.NewsGroup, TypeAudit.Edit)]
    public async Task<IActionResult> Edit([FromBody] NewsGroupEventModel model)
    {
        if (ModelState.IsValid) return NResponseCommand(await _newsGroupAppService.Update(model), model);
        NotifyModelStateErrors();
        return NResponseCommand(false, model);

    }

    /// <summary>
    ///     Xóa Nhóm tin
    /// </summary>
    /// <param name="model">danh sách Id xóa</param>
    /// <returns></returns>
    [HttpPost("delete")]
    [CustomAuthorize(ModuleIdentity.NewsGroup, TypeAudit.Delete)]
    public async Task<IActionResult> Delete([FromBody] DeleteModal model)
    {
        return NResponseCommand(await _newsGroupAppService.Delete(model), model);
    }

    /// <summary>
    ///     Lấy Nhóm tin theo Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("get-by-id/{id:guid}")]
    [CustomAuthorize(ModuleIdentity.NewsGroup, TypeAudit.View)]
    public IActionResult GetById(Guid id)
    {
        return NResponseCommand(null, _newsGroupAppService.GetById(id));
    }

    /// <summary>
    ///     [Treeview] Trả về tree-view
    /// </summary>
    /// <param name="vSearch">Giá trị tìm kiếm</param>
    /// <param name="newsGroupTypeId">Mã loại nhóm tin</param>
    /// <param name="isAll">Hiển thị tất cả</param>
    /// <returns></returns>
    // [CustomAuthorize(ModuleIdentity.MenuManager, TypeAudit.View)]
    [HttpGet("get-treeview")]
    public async Task<JsonResponse<IEnumerable<TreeViewProjectModel>>> GetTreeViewRoot(
        string vSearch, Guid newsGroupTypeId, bool isAll)
    {
        return new OkResponse<IEnumerable<TreeViewProjectModel>>(
            "", await _newsGroupAppService.GetTreeView(new TreeViewPagingModel
            {
                IsAll           = isAll,
                ModuleIdentity  = ModuleIdentity.NewsGroup,
                VSearch         = vSearch,
                NewsGroupTypeId = newsGroupTypeId,
                IsModal         = false
            }));
    }

    /// <summary>
    ///     [Treeview-Modal] Trả về tree-view
    /// </summary>
    /// <param name="vSearch">Giá trị tìm kiếm</param>
    /// <param name="newsGroupTypeId">Mã loại nhóm tin</param>
    /// <param name="isAll">Hiển thị tất cả</param>
    /// <returns></returns>
    [HttpGet("get-treeview-modal")]
    [CustomAuthorize(ModuleIdentity.Helpers, TypeAudit.ShowModal)]
    public async Task<JsonResponse<IEnumerable<TreeViewProjectModel>>> GetTreeViewModal(
        string vSearch, Guid newsGroupTypeId, bool isAll)
    {
        return new OkResponse<IEnumerable<TreeViewProjectModel>>(
            "", await _newsGroupAppService.GetTreeView(new TreeViewPagingModel
            {
                IsAll           = isAll,
                ModuleIdentity  = "",
                VSearch         = vSearch,
                NewsGroupTypeId = newsGroupTypeId,
                IsModal         = true
            }));
    }

    /// <summary>
    ///     Danh sách Vercel
    /// </summary>
    /// <returns></returns>
    [HttpGet("get-vercel")]
    [CustomAuthorize(ModuleIdentity.Helpers, TypeAudit.ShowModal)]
    public async Task<JsonResponse<IEnumerable<ListVercelDto>>> GetVercel()
    {
        return new OkResponse<IEnumerable<ListVercelDto>>(
            "",
            await _newsGroupAppService.ListDomainVercel());
    }
    
    /// <summary>
    ///     Thay đổi Domain Vercel
    /// </summary>
    /// <returns></returns>
    [HttpGet("change-domain-vercel")]
    [CustomAuthorize(ModuleIdentity.Helpers, TypeAudit.ShowModal)]
    public async Task<JsonResponse<string>> GetDomainVercel()
    {
        return new OkResponse<string>(
            "",
            await _newsGroupAppService.ChangeDomainVercel());
    }

    #endregion
}