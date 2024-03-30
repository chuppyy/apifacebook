#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITC.Application.AppService.AuthorityManager.AuthorityManagerSystem;
using ITC.Application.AppService.AuthorityManager.MenuManagerSystem;
using ITC.Application.Helpers;
using ITC.Domain.Core.Bus;
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

namespace ITC.Service.API.Controllers.AuthorityManager;

/// <summary>
///     Phân quyền
/// </summary>
[Route("[controller]")]
[ApiController]
[Authorize]
public class AuthorityManagerSystemController : ApiController
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
    public AuthorityManagerSystemController(INotificationHandler<DomainNotification> notifications,
                                            IMediatorHandler                         mediator,
                                            IUser                                    user,
                                            IAuthorityManagerAppService              authorityManagerAppService,
                                            IMenuManagerAppService                   menuManagerAppService) :
        base(notifications, mediator)
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
    [CustomAuthorize(ModuleIdentity.AuthorityManager, TypeAudit.Add)]
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
    [CustomAuthorize(ModuleIdentity.AuthorityManager, TypeAudit.Edit)]
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
    [CustomAuthorize(ModuleIdentity.AuthorityManager, TypeAudit.Delete)]
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
    [CustomAuthorize(ModuleIdentity.AuthorityManager, TypeAudit.View)]
    public IActionResult GetById(Guid id)
    {
        return NResponseCommand(null, _authorityManagerAppService.GetById(id));
    }

    /// <summary>
    ///     [Phân trang] Danh sách quyền sử dụng
    /// </summary>
    /// <returns></returns>
    [CustomAuthorize(ModuleIdentity.AuthorityManager, TypeAudit.View)]
    [HttpGet("get-paging")]
    public async Task<JsonResponse<Pagination<AuthorityManagerSystemPagingDto>>> GetPaging(
        string search, int pageSize, int pageNumber)
    {
        return await Task.Run(() =>
        {
            var lData =
                _authorityManagerAppService.GetPaging(new PagingModel
                {
                    Search         = search,
                    PageNumber     = pageNumber,
                    PageSize       = pageSize,
                    ProjectId      = _user.ProjectId,
                    ModuleIdentity = ModuleIdentity.AuthorityManager
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
            new OkResponse<IEnumerable<ComboboxModal>>
            ("",
             await _authorityManagerAppService.GetCombobox(search, projectId));
    }

    /// <summary>
    ///     [Treeview] Trả về danh sách quyền sử dụng theo authority để phân quyền
    /// </summary>
    /// <returns></returns>
    // [CustomAuthorize(ModuleIdentity.MenuManager, TypeAudit.View)]
    [HttpGet("get-feature-by-authorities")]
    public async Task<JsonResponse<IEnumerable<MenuRoleEventViewModel>>> GetFeatureByAuthorities(string authorities)
    {
        return new OkResponse<IEnumerable<MenuRoleEventViewModel>>(
            "",
            await _menuManagerAppService
                .GetMenuByAuthorities(string.Compare(authorities, "0", StringComparison.Ordinal) == 0
                                          ? Guid.Empty
                                          : Guid.Parse(authorities),
                                      false));
    }

#endregion

#region Fields

    private readonly IUser                       _user;
    private readonly IAuthorityManagerAppService _authorityManagerAppService;
    private readonly IMenuManagerAppService      _menuManagerAppService;

#endregion
}