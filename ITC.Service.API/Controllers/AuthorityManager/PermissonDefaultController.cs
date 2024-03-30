#region

using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Application.AppService.AuthorityManager.MenuManagerSystem;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare;
using ITC.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NCore.Responses;
using NCore.Systems;

#endregion

namespace ITC.Service.API.Controllers.AuthorityManager;

/// <summary>
///     Quyền mặc định
/// </summary>
[Route("[controller]")]
[ApiController]
[Authorize]
public class PermissonDefaultController : ApiController
{
#region Fields

    private readonly IMenuManagerAppService _appService;

#endregion

#region Constructors

    /// <summary>
    ///     Hàm dựng
    /// </summary>
    /// <param name="appService"></param>
    /// <param name="notifications"></param>
    /// <param name="mediator"></param>
    public PermissonDefaultController(IMenuManagerAppService                   appService,
                                      INotificationHandler<DomainNotification> notifications,
                                      IMediatorHandler                         mediator) :
        base(notifications, mediator)
    {
        _appService = appService;
    }

#endregion

#region Methods

    /// <summary>
    ///     Danh sách quyền mặc định
    /// </summary>
    /// <returns></returns>
    [HttpGet("get-permission")]
    public async Task<JsonResponse<IEnumerable<PermissionDefaultViewModal>>> GetPermission(int value)
    {
        return new
            OkResponse<IEnumerable<PermissionDefaultViewModal>>("", await _appService.GetPermissionDefault(value));
    }

    /// <summary>
    ///     Danh sách quyền mặc định
    /// </summary>
    /// <returns></returns>
    [HttpGet("v3/permission-default")]
    public async Task<JsonResponse<IEnumerable<v2023PermissionDefaultViewModal>>> GetPermissionDefault(int value)
    {
        return new
            OkResponse<IEnumerable<v2023PermissionDefaultViewModal>>(
                "", await _appService.V2023GetPermissionDefault(value));
    }

#endregion
}