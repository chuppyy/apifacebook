#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITC.Application.AppService.CompanyManagers.StaffManagers;
using ITC.Application.Helpers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.CompanyManager.StaffManagers;
using ITC.Domain.Core.NCoreLocal.Enum;
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
///     người dùng
/// </summary>
[Route("[controller]")]
[ApiController]
[Authorize]
public class UserManagerController : ApiController
{
#region Fields

    private readonly IStaffManagerAppService _staffManagerAppService;

#endregion

#region Constructors

    /// <summary>
    ///     Hàm dựng
    /// </summary>
    /// <param name="staffManagerAppService"></param>
    /// <param name="notifications"></param>
    /// <param name="mediator"></param>
    public UserManagerController(IStaffManagerAppService                  staffManagerAppService,
                                 INotificationHandler<DomainNotification> notifications,
                                 IMediatorHandler                         mediator) :
        base(notifications, mediator)
    {
        _staffManagerAppService = staffManagerAppService;
    }

#endregion

#region Methods

    /// <summary>
    ///     Thêm mới người dùng
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("create")]
    [CustomAuthorize(ModuleIdentity.UserManager, TypeAudit.Add)]
    public async Task<IActionResult> Add([FromBody] StaffManagerEventModel model)
    {
        if (ModelState.IsValid) return NResponseCommand(await _staffManagerAppService.Add(model), model);
        NotifyModelStateErrors();
        return NResponseCommand(false, model);

    }

    /// <summary>
    ///     Sửa người dùng
    /// </summary>
    /// <param name="model">Model menu</param>
    /// <returns></returns>
    [HttpPut("update")]
    [CustomAuthorize(ModuleIdentity.UserManager, TypeAudit.Edit)]
    public async Task<IActionResult> Edit([FromBody] StaffManagerEventModel model)
    {
        if (ModelState.IsValid) return NResponseCommand(await _staffManagerAppService.Update(model), model);
        NotifyModelStateErrors();
        return NResponseCommand(false, model);

    }

    /// <summary>
    ///     Xóa người dùng
    /// </summary>
    /// <param name="model">danh sách Id xóa</param>
    /// <returns></returns>
    [HttpPost("delete")]
    [CustomAuthorize(ModuleIdentity.UserManager, TypeAudit.Delete)]
    public async Task<IActionResult> Delete([FromBody] DeleteModal model)
    {
        return NResponseCommand(await _staffManagerAppService.Delete(model), model);
    }

    /// <summary>
    ///     Lấy người dùng theo Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("get-by-id/{id:guid}")]
    [CustomAuthorize(ModuleIdentity.UserManager, TypeAudit.View)]
    public IActionResult GetById(Guid id)
    {
        return NResponseCommand(null, _staffManagerAppService.GetById(id));
    }

    /// <summary>
    ///     [Phân trang] Danh sách người dùng
    /// </summary>
    /// <returns></returns>
    [CustomAuthorize(ModuleIdentity.UserManager, TypeAudit.View)]
    [HttpGet("get-paging")]
    public async Task<JsonResponse<Pagination<StaffManagerPagingDto>>> GetPaging(
        string search, int pageSize, int pageNumber)
    {
        return await Task.Run(() =>
        {
            var lData = _staffManagerAppService.GetPaging(new StaffManagerPagingViewModel
            {
                Search            = search,
                PageNumber        = pageNumber,
                PageSize          = pageSize,
                RoomManagerId     = Guid.Empty,
                UserTypeManagerId = Guid.Empty,
                ModuleIdentity    = ModuleIdentity.UserManager
            }, GroupTableEnum.UserManager.Id).Result.ToList();
            return new OkResponse<Pagination<StaffManagerPagingDto>>("",
                                                                     new Pagination<StaffManagerPagingDto>
                                                                     {
                                                                         PageLists = lData,
                                                                         TotalRecord =
                                                                             lData.Count > 0
                                                                                 ? lData[0].TotalRecord
                                                                                 : 0
                                                                     });
        });
    }


    /// <summary>
    ///     [Phân trang] Danh sách người dùng
    /// </summary>
    /// <returns></returns>
    [CustomAuthorize(ModuleIdentity.UserManager, TypeAudit.View)]
    [HttpGet("get-paging-user-tracing")]
    public async Task<JsonResponse<Pagination<UserTracingPagingDto>>> GetPagingUserTracing(
        string search, int pageSize, int pageNumber)
    {
        return await Task.Run(() =>
        {
            var lData = _staffManagerAppService.GetPagingUserTracing(new PagingModel
            {
                Search         = search,
                PageNumber     = pageNumber,
                PageSize       = pageSize,
                ModuleIdentity = ModuleIdentity.UserManager
            }).Result.ToList();
            return new OkResponse<Pagination<UserTracingPagingDto>>("",
                                                                    new Pagination<UserTracingPagingDto>
                                                                    {
                                                                        PageLists = lData,
                                                                        TotalRecord =
                                                                            lData.Count > 0
                                                                                ? lData[0].TotalRecord
                                                                                : 0
                                                                    });
        });
    }

    /// <summary>
    ///     Combobox
    /// </summary>
    /// <returns></returns>
    [HttpGet("get-combobox")]
    public async Task<JsonResponse<IEnumerable<ComboboxModal>>> GetCombobox(string search)
    {
        return
            new OkResponse<IEnumerable<ComboboxModal>>("", await _staffManagerAppService.GetCombobox(search));
    }

#endregion
}