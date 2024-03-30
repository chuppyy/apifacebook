#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Application.AppService.SystemManagers.SortMenuManagers;
using ITC.Application.Helpers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.SystemManagers.SortMenuManagers;
using ITC.Domain.Core.Notifications;
using ITC.Infra.CrossCutting.Identity.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NCore.Modals;
using NCore.Responses;
using NCore.Systems;

#endregion

namespace ITC.Service.API.Controllers.SystemManagers;

/// <summary>
///     Sắp xếp menu
/// </summary>
[Route("[controller]")]
[ApiController]
[Authorize]
public class SortMenuManagerController : ApiController
{
#region Fields

    private readonly ISortMenuManagerAppService _sortMenuManagerAppService;

#endregion

#region Constructors

    /// <summary>
    ///     Hàm dựng
    /// </summary>
    /// <param name="sortMenuManagerAppService"></param>
    /// <param name="notifications"></param>
    /// <param name="mediator"></param>
    public SortMenuManagerController(ISortMenuManagerAppService               sortMenuManagerAppService,
                                     INotificationHandler<DomainNotification> notifications,
                                     IMediatorHandler                         mediator) :
        base(notifications, mediator)
    {
        _sortMenuManagerAppService = sortMenuManagerAppService;
    }

#endregion

#region Methods

    /// <summary>
    ///     Sắp xếp menu
    /// </summary>
    /// <param name="model">Model menu</param>
    /// <returns></returns>
    [HttpPut("update")]
    [CustomAuthorize(ModuleIdentity.SortMenuManager, TypeAudit.Edit)]
    public async Task<IActionResult> Edit([FromBody] SortMenuManagerEventModel model)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return NResponseCommand(false, model);
        }

        return NResponseCommand(await _sortMenuManagerAppService.Update(model), model);
    }

    /// <summary>
    ///     Xóa menu
    /// </summary>
    /// <param name="model">danh sách Id xóa</param>
    /// <returns></returns>
    [HttpPost("delete")]
    [CustomAuthorize(ModuleIdentity.SortMenuManager, TypeAudit.Delete)]
    public async Task<IActionResult> Delete([FromBody] DeleteModal model)
    {
        return NResponseCommand(await _sortMenuManagerAppService.Delete(model), model);
    }

    /// <summary>
    ///     [Phân trang] Danh sách
    /// </summary>
    /// <returns></returns>
    [CustomAuthorize(ModuleIdentity.SortMenuManager, TypeAudit.View)]
    [HttpGet("get-paging")]
    public async Task<JsonResponse<IEnumerable<SortMenuManagerDto>>> GetPaging(Guid menuId, Guid parentId)
    {
        return new OkResponse<IEnumerable<SortMenuManagerDto>>("",
                                                               await _sortMenuManagerAppService.GetSortMenu(
                                                                   menuId, parentId));
    }

#endregion
}