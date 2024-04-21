#region

using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Application.AppService.CompanyManagers.StaffManagers;
using ITC.Application.AppService.SystemManagers.HelperManagers;
using ITC.Application.Helpers;
using ITC.Domain.Commands.CompanyManagers.StaffManager;
using ITC.Domain.Commands.GoogleAnalytics.Models;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.CompanyManager.StaffManagers;
using ITC.Domain.Core.Notifications;
using ITC.Domain.ResponseDto;
using ITC.Infra.CrossCutting.Identity.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NCore.Responses;
using NCore.Systems;

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
    /// <param name="helperAppService"></param>
    public StaffManagerController(IStaffManagerAppService                  staffManagerAppService,
                                  INotificationHandler<DomainNotification> notifications,
                                  IMediatorHandler                         mediator, IHelperAppService helperAppService) :
        base(notifications, mediator)
    {
        _staffManagerAppService = staffManagerAppService;
        _helperAppService = helperAppService;
    }

    #endregion

    #region Fields

    private readonly IStaffManagerAppService _staffManagerAppService;
    private readonly IHelperAppService _helperAppService;

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

    /// <summary>
    /// Lấy danh sách người dùng thuộc bởi quản lý
    /// </summary>
    /// <returns></returns>
    [HttpGet("list-by-owner")]
    public async Task<JsonResponse<IEnumerable<UserByOwnerDto>>> GetListUserAsync([FromQuery] GetListUserQuery query)
    {
        return new OkResponse<IEnumerable<UserByOwnerDto>>("", await _helperAppService.GetListUserAsync(query));
    }

    /// <summary>
    /// Lấy danh sách người dùng thuộc bởi quản lý
    /// </summary>
    /// <returns></returns>
    [HttpGet("update-ratio")]
    public async Task<JsonResponse<bool>> UpdateRatioUserAsync(UpdateRatioUserCommand query)
    {
        return new OkResponse<bool>("", await _helperAppService.UpdateRatioUserAsync(query));
    }
}