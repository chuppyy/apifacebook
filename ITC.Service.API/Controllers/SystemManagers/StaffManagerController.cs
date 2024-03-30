#region

using System.Threading.Tasks;
using ITC.Application.AppService.CompanyManagers.StaffManagers;
using ITC.Application.Helpers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.CompanyManager.StaffManagers;
using ITC.Domain.Core.Notifications;
using ITC.Infra.CrossCutting.Identity.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace ITC.Service.API.Controllers.SystemManagers;

/// <summary>
///     nhân viên
/// </summary>
[Route("[controller]")]
[ApiController]
[Authorize]
public class StaffManagerController : ApiController
{
    #region Constructors

    /// <summary>
    ///     Hàm dựng
    /// </summary>
    /// <param name="staffManagerAppService"></param>
    /// <param name="notifications"></param>
    /// <param name="mediator"></param>
    public StaffManagerController(IStaffManagerAppService                  staffManagerAppService,
                                  INotificationHandler<DomainNotification> notifications,
                                  IMediatorHandler                         mediator) :
        base(notifications, mediator)
    {
        _staffManagerAppService = staffManagerAppService;
    }

    #endregion

    #region Fields

    private readonly IStaffManagerAppService _staffManagerAppService;

    #endregion

    /// <summary>
    ///     Cập nhật ảnh đại diện
    /// </summary>
    /// <returns></returns>
    [HttpPost("update-avatar")]
    [CustomAuthorize(ModuleIdentity.Helpers, TypeAudit.Edit)]
    public async Task<IActionResult> UpdateAvatar(UploadImageStaffEventModel model)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return CustomResponse("Dữ liệu không chính xác");
        }

        return NResponse(await _staffManagerAppService.AvatarManager(model)
                             ? "Cập nhật ảnh đại diện không thành công"
                             : "Cập nhật ảnh đại diện thành công");
    }
}