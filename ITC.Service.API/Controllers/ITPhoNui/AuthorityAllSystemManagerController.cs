#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITC.Application.AppService.AuthorityManager.AuthorityManagerSystem;
using ITC.Application.AppService.AuthorityManager.MenuManagerSystem;
using ITC.Application.Helpers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare;
using ITC.Domain.Core.ModelShare.AuthorityManager;
using ITC.Domain.Core.ModelShare.AuthorityManager.AuthorityManagerSystems;
using ITC.Domain.Core.Notifications;
using ITC.Domain.Interfaces;
using ITC.Infra.CrossCutting.Identity.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NCore.Modals;
using NCore.Responses;
using NCore.Systems;

#endregion

namespace ITC.Service.API.Controllers.ITPhoNui;

/// <summary>
///     Phân quyền quản trị toàn hệ thống
/// </summary>
[Route("[controller]")]
[ApiController]
[Authorize]
public class AuthorityAllSystemManagerController : ApiController
{
#region Constructors

    /// <summary>
    ///     Hàm dựng
    /// </summary>
    /// <param name="notifications"></param>
    /// <param name="mediator"></param>
    /// <param name="user"></param>
    /// <param name="authorityManagerAppService"></param>
    /// <param name="menuManagerAppService"></param>
    public AuthorityAllSystemManagerController(
        INotificationHandler<DomainNotification> notifications,
        IMediatorHandler                         mediator,
        IUser                                    user,
        IAuthorityManagerAppService              authorityManagerAppService,
        IMenuManagerAppService                   menuManagerAppService) : base(notifications, mediator)
    {
        _user                       = user;
        _authorityManagerAppService = authorityManagerAppService;
        _menuManagerAppService      = menuManagerAppService;
    }

#endregion

#region =================================================AUTHORYTY==================================================

    /// <summary>
    ///     Thêm mới quyền sử dụng
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("create")]
    [CustomAuthorize(ModuleIdentity.AuthorityManagerAllSystem, TypeAudit.Add)]
    public async Task<IActionResult> Add([FromBody] AuthorityManagerSystemEventModel model)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return NResponseCommand(false, model);
        }

        model.CreatedBy = _user.UserId;
        return NResponseCommand(await _authorityManagerAppService.Add(model), model);
    }

    /// <summary>
    ///     Sửa quyền sử dụng
    /// </summary>
    /// <param name="model">Model menu</param>
    /// <returns></returns>
    [HttpPut("update")]
    [CustomAuthorize(ModuleIdentity.AuthorityManagerAllSystem, TypeAudit.Edit)]
    public async Task<IActionResult> Edit([FromBody] AuthorityManagerSystemEventModel model)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return NResponseCommand(false, model);
        }

        model.CreatedBy = _user.UserId;
        return NResponseCommand(await _authorityManagerAppService.Update(model), model);
    }

    /// <summary>
    ///     Xóa quyền sử dụng
    /// </summary>
    /// <param name="model">danh sách Id xóa</param>
    /// <returns></returns>
    [HttpPost("delete")]
    [CustomAuthorize(ModuleIdentity.AuthorityManagerAllSystem, TypeAudit.Delete)]
    public async Task<IActionResult> Delete([FromBody] DeleteModal model)
    {
        return NResponseCommand(await _authorityManagerAppService.Delete(model), model);
    }

    /// <summary>
    ///     Lấy quyền sử dụng theo Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("get-by-id/{id:guid}")]
    [CustomAuthorize(ModuleIdentity.AuthorityManagerAllSystem, TypeAudit.View)]
    public IActionResult GetById(Guid id)
    {
        return NResponseCommand(null, _authorityManagerAppService.GetById(id));
    }

    /// <summary>
    ///     [Phân trang] Danh sách quyền sử dụng
    /// </summary>
    /// <returns></returns>
    [CustomAuthorize(ModuleIdentity.AuthorityManagerAllSystem, TypeAudit.View)]
    [HttpGet("get-paging")]
    public async Task<JsonResponse<Pagination<AuthorityManagerSystemPagingDto>>> GetPaging(
        string search, int pageSize, int pageNumber, Guid projectId, int statusId)
    {
        return await Task.Run(() =>
        {
            var lData = _authorityManagerAppService.GetPaging(new PagingModel
            {
                Search         = search,
                PageNumber     = pageNumber,
                PageSize       = pageSize,
                ProjectId      = projectId,
                StatusId       = statusId,
                ModuleIdentity = ModuleIdentity.AuthorityManagerAllSystem
            }).Result.ToList();
            return new OkResponse<Pagination<AuthorityManagerSystemPagingDto>>("",
                                                                               new Pagination<
                                                                                   AuthorityManagerSystemPagingDto>
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
    public async Task<JsonResponse<IEnumerable<ComboboxModal>>> GetCombobox(string search, Guid? projectId)
    {
        return
            new OkResponse<IEnumerable<ComboboxModal>>("",
                                                       await _authorityManagerAppService
                                                           .GetCombobox(search, projectId));
    }

    /// <summary>
    ///     [Treeview] Trả về danh sách quyền sử dụng theo authority để phân quyền
    /// </summary>
    /// <returns></returns>
    [CustomAuthorize(ModuleIdentity.AuthorityManagerAllSystem, TypeAudit.View)]
    [HttpGet("v3/get-feature-by-authorities")]
    public async Task<JsonResponse<IEnumerable<MenuByAuthoritiesV2023>>> V202301GetFeatureByAuthorities(
        Guid authorities)
    {
        return new OkResponse<IEnumerable<MenuByAuthoritiesV2023>>("",
                                                                   await _menuManagerAppService
                                                                       .V202301GetMenuByAuthorities(authorities));
    }

    /// <summary>
    ///     Trả về các chức năng theo parrentId
    /// </summary>
    /// <returns></returns>
    [CustomAuthorize(ModuleIdentity.AuthorityManagerAllSystem, TypeAudit.View)]
    [HttpGet("v3/get-feature-by-authorities-menu")]
    public async Task<JsonResponse<IEnumerable<MenuByAuthoritiesV2023>>> V202301GetFeatureByAuthoritiesParent(
        Guid authorities, Guid menuId)
    {
        return new OkResponse<IEnumerable<MenuByAuthoritiesV2023>>("",
                                                                   await _menuManagerAppService
                                                                       .V202301GetFeatureByAuthoritiesParent(
                                                                           authorities,
                                                                           menuId));
    }

    /// <summary>
    ///     Trả về giá trị mặc định của chức năng
    /// </summary>
    /// <returns></returns>
    [CustomAuthorize(ModuleIdentity.AuthorityManagerAllSystem, TypeAudit.View)]
    [HttpGet("v3/get-permission-default-by-menu")]
    public async Task<JsonResponse<IEnumerable<v2023PermissionDefaultViewModal>>> V202301GetPermissionDefaultByMenu(
        Guid authorities, Guid menuId, int value)
    {
        return new OkResponse<IEnumerable<v2023PermissionDefaultViewModal>>("",
                                                                            await _menuManagerAppService
                                                                                .V202301GetPermissionDefaultByMenu(
                                                                                    authorities,
                                                                                    menuId, value));
    }

    /// <summary>
    ///     Cập nhật quyền sử dụng menu
    /// </summary>
    /// <param name="model">Model menu</param>
    /// <returns></returns>
    [HttpPut("v3/update-permission-menu")]
    [CustomAuthorize(ModuleIdentity.AuthorityManagerAllSystem, TypeAudit.Edit)]
    public async Task<IActionResult> UpdatePermissionMenu(
        [FromBody] AuthorityManagerSystemUpdatePermissionEventModel model)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return NResponseCommand(false, model);
        }

        return NResponseCommand(await _authorityManagerAppService.UpdatePermissionMenu(model), model);
    }

#endregion

#region Fields

    private readonly IUser                       _user;
    private readonly IAuthorityManagerAppService _authorityManagerAppService;
    private readonly IMenuManagerAppService      _menuManagerAppService;

#endregion
}