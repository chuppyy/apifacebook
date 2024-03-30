#region

using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Application.AppService.SystemManagers.HelperManagers;
using ITC.Application.Helpers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.SystemManagers.HelperManagers;
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
///     Helper
/// </summary>
[Route("[controller]")]
[ApiController]
[Authorize]
public class HelperController : ApiController
{
#region Fields

    private readonly IHelperAppService _helperAppService;

#endregion

#region Constructors

    /// <summary>
    ///     Hàm dựng
    /// </summary>
    /// <param name="helperAppService"></param>
    /// <param name="notifications"></param>
    /// <param name="mediator"></param>
    public HelperController(IHelperAppService                        helperAppService,
                            INotificationHandler<DomainNotification> notifications,
                            IMediatorHandler                         mediator) :
        base(notifications, mediator)
    {
        _helperAppService = helperAppService;
    }

#endregion

    /// <summary>
    ///     [Combobox] Danh sách trạng thái hiển thị nội dung đính kèm trong bảng nội dung
    /// </summary>
    /// <returns></returns>
    [HttpGet("get-combobox-news-attack")]
    public async Task<JsonResponse<IEnumerable<ComboboxModalInt>>> GetComboboxNewsAttack()
    {
        return
            new OkResponse<IEnumerable<ComboboxModalInt>>("", await _helperAppService.GetAttackViewCombobox());
    }

    /// <summary>
    ///     Cập nhật trạng thái dữ liệu
    /// </summary>
    /// <returns></returns>
    [HttpPut("update-status")]
    [CustomAuthorize(ModuleIdentity.Helpers, TypeAudit.UpdateStatus)]
    public async Task<IActionResult> PutUpdateStatusAsync([FromBody] UpdateStatusHelperModal model)
    {
        if (ModelState.IsValid) return NResponseCommand(await _helperAppService.UpdateStatus(model), model);
        NotifyModelStateErrors();
        return NResponseCommand(false, model);

    }

    /// <summary>
    ///     Kiểm tra thời gian hiệu chỉnh dữ liệu
    /// </summary>
    /// <returns></returns>
    [HttpPost("check-time")]
    [CustomAuthorize(ModuleIdentity.Helpers, TypeAudit.UpdateStatus)]
    public async Task<IActionResult> GetCheckTime(CheckTimeModel model)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return NResponseCommand(false);
        }

        return NResponseCommand(await _helperAppService.CheckTime(model), model);
    }
}