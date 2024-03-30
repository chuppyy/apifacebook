#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Application.AppService.NewsManagers.NewsAttackManagers;
using ITC.Application.Helpers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsAttackManagers;
using ITC.Domain.Core.Notifications;
using ITC.Infra.CrossCutting.Identity.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NCore.Responses;
using NCore.Systems;

#endregion

namespace ITC.Service.API.Controllers.NewsManagers;

/// <summary>
///     Đính kèm bài viết
/// </summary>
[Route("[controller]")]
[ApiController]
[Authorize]
public class NewsAttackController : ApiController
{
#region Fields

    private readonly INewsAttackAppService _newsAttackAppService;

#endregion

#region Constructors

    /// <summary>
    ///     Hàm dựng
    /// </summary>
    /// <param name="newsAttackAppService"></param>
    /// <param name="notifications"></param>
    /// <param name="mediator"></param>
    public NewsAttackController(INewsAttackAppService                    newsAttackAppService,
                                INotificationHandler<DomainNotification> notifications,
                                IMediatorHandler                         mediator) :
        base(notifications, mediator)
    {
        _newsAttackAppService = newsAttackAppService;
    }

#endregion

#region Methods

    /// <summary>
    ///     Danh sách file đính kèm
    /// </summary>
    /// <param name="id">mã bài viết</param>
    /// <returns></returns>
    [CustomAuthorize(ModuleIdentity.Helpers, TypeAudit.View)]
    [HttpGet("get-paging")]
    public async Task<JsonResponse<IEnumerable<NewsAttackPagingDto>>> GetPaging(Guid id)
    {
        return new
            OkResponse<IEnumerable<NewsAttackPagingDto>>("", await _newsAttackAppService.GetPaging(id));
    }

#endregion
}