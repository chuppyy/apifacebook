#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITC.Application.AppService.AuthorityManager.MenuManagerSystem;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.AuthorityManager;
using ITC.Domain.Core.Notifications;
using ITC.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NCore.Modals;
using NCore.Responses;
using NCore.Systems;

#endregion

namespace ITC.Service.API.Controllers.AuthorityManager;

/// <summary>
///     Menu Admin
/// </summary>
[Route("[controller]")]
[ApiController]
[Authorize]
public class MenuManagerController : ApiController
{
#region Constructors

    /// <summary>
    ///     Hàm dựng
    /// </summary>
    /// <param name="user"></param>
    /// <param name="menuManagerAppService"></param>
    /// <param name="notifications"></param>
    /// <param name="mediator"></param>
    public MenuManagerController(IUser                                    user,
                                 IMenuManagerAppService                   menuManagerAppService,
                                 INotificationHandler<DomainNotification> notifications,
                                 IMediatorHandler                         mediator) :
        base(notifications, mediator)
    {
        _user                  = user;
        _menuManagerAppService = menuManagerAppService;
    }

#endregion

#region Fields

    private readonly IMenuManagerAppService _menuManagerAppService;
    private readonly IUser                  _user;

#endregion

#region Methods

    /// <summary>
    ///     Thêm mới
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("create")]
    // [CustomAuthorize(ModuleIdentity.MenuManager, TypeAudit.Add)]
    public IActionResult Add([FromBody] MenuManagerEventModel model)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return NResponse(model);
        }

        model.ManagementId = _user.ManagementUnitId;
        model.CreatedBy    = _user.UserId;
        _menuManagerAppService.Add(model);

        return NResponse(model);
    }

    /// <summary>
    ///     Sửa thông tin menu
    /// </summary>
    /// <param name="model">Model menu</param>
    /// <returns></returns>
    [HttpPut("update")]
    // [CustomAuthorize(ModuleIdentity.MenuManager, TypeAudit.Edit)]
    public IActionResult Edit([FromBody] MenuManagerEventModel model)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return NResponse(model);
        }

        model.ManagementId = _user.ManagementUnitId;
        model.CreatedBy    = _user.UserId;
        _menuManagerAppService.Update(model);

        return NResponse(model);
    }

    /// <summary>
    ///     Xóa menu
    /// </summary>
    /// <param name="ids">danh sách Id xóa</param>
    /// <returns></returns>
    [HttpDelete("delete")]
    // [CustomAuthorize(ModuleIdentity.MenuManager, TypeAudit.Delete)]
    public IActionResult Delete(Guid ids)
    {
        _menuManagerAppService.Delete(ids);
        return NResponse();
    }

    /// <summary>
    ///     Lấy menu theo Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("get-by-id/{id:guid}")]
    // [CustomAuthorize(ModuleIdentity.MenuManager, TypeAudit.View)]
    public IActionResult GetById(Guid id)
    {
        var model = _menuManagerAppService.GetById(id);
        return NResponse(model);
    }

    /// <summary>
    ///     [Treeview] Trả về Root
    /// </summary>
    /// <returns></returns>
    // [CustomAuthorize(ModuleIdentity.MenuManager, TypeAudit.View)]
    [HttpGet("get-treeview")]
    public async Task<JsonResponse<IEnumerable<TreeViewProjectModel>>> GetTreeViewRoot(int version)
    {
        return new OkResponse<IEnumerable<TreeViewProjectModel>>("", await _menuManagerAppService.GetTreeView(version));
    }

    /// <summary>
    ///     [Phân trang] Danh sách quyền sử dụng
    /// </summary>
    /// <returns></returns>
    // [CustomAuthorize(ModuleIdentity.MenuManager, TypeAudit.View)]
    [HttpGet("get-paging-authorities")]
    public async Task<JsonResponse<Pagination<AuthoritiesViewModel>>> GetPagingAuthorities(
        Guid companyId, Guid projectId, string search, int pageSize, int pageNumber)
    {
        return await Task.Run(() =>
        {
            var lData = _menuManagerAppService
                        .GetAuthoritiesAsync(companyId, projectId, search, pageSize,
                                             pageNumber).Result
                        .ToList();
            return new OkResponse<Pagination<AuthoritiesViewModel>>("",
                                                                    new Pagination<AuthoritiesViewModel>
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
    ///     Thêm mới phân quyền
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("create-authorities")]
    // [CustomAuthorize(ModuleIdentity.MenuManager, TypeAudit.Add)]
    public IActionResult Add([FromBody] AuthoritiesMenuManagerEventModel model)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return NResponse(model);
        }

        _menuManagerAppService.AddAuthorities(model);
        return NResponse(model);
    }

    /// <summary>
    ///     Sửa thông tin menu
    /// </summary>
    /// <param name="model">Model menu</param>
    /// <returns></returns>
    [HttpPut("update-authorities")]
    // [CustomAuthorize(ModuleIdentity.MenuManager, TypeAudit.Edit)]
    public IActionResult EditAuthorities([FromBody] AuthoritiesMenuManagerEventModel model)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return NResponse(model);
        }

        _menuManagerAppService.UpdateAuthorities(model);

        return NResponse(model);
    }

    /// <summary>
    ///     Combobox
    /// </summary>
    /// <returns></returns>
    [HttpGet("get-combobox-authorities")]
    public async Task<JsonResponse<IEnumerable<KeyValuePair<Guid, string>>>> GetCombobox(
        Guid companyId, Guid projectId)
    {
        return
            new OkResponse<IEnumerable<KeyValuePair<Guid, string>>>("",
                                                                    await _menuManagerAppService
                                                                        .GetAuthoritiesCombobox(companyId,
                                                                            projectId));
    }

    /// <summary>
    ///     Lấy dữ liệu quyền sử dụng theo Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("get-authorities-by-id/{id:guid}")]
    // [CustomAuthorize(ModuleIdentity.MenuManager, TypeAudit.View)]
    public IActionResult GetAuthoritiesById(Guid id)
    {
        var model = _menuManagerAppService.GetAuthoritiesById(id).Result;
        return NResponse(model);
    }

    /// <summary>
    ///     Xóa quyền sử dụng
    /// </summary>
    /// <param name="model">danh sách Id xóa</param>
    /// <param name="projectId"></param>
    /// <param name="companyId"></param>
    /// <returns></returns>
    [HttpDelete("delete-authorities")]
    // [CustomAuthorize(ModuleIdentity.MenuManager, TypeAudit.Delete)]
    public IActionResult DeleteAuthorities(string model, Guid projectId, Guid companyId)
    {
        _menuManagerAppService.DeleteAuthorities(model, projectId, companyId);
        return NResponse();
    }

    /// <summary>
    ///     Lấy dữ liệu quyền truy cập theo ID chức năng đã được cấp
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("get-permission-by-authorities")]
    public async Task<JsonResponse<IEnumerable<PermissionByAuthoritiesModel>>> GetPermissionByAuthorities(Guid id)
    {
        return new OkResponse<IEnumerable<PermissionByAuthoritiesModel>>(
            "", await _menuManagerAppService.GetPermissionByAuthorities(id));
    }


    /// <summary>
    ///     Cập nhật giá trị quyền
    /// </summary>
    /// <param name="model">Model menu</param>
    /// <returns></returns>
    [HttpPost("update-permission-by-authorities")]
    public async Task<IActionResult> UpdatePermissionByAuthorities(
        [FromBody] SortMenuPermissionByAuthoritiesModel model)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return NResponse(model);
        }

        return NResponseCommand(await _menuManagerAppService.UpdatePermissionByAuthorities(model), model);
    }

    /// <summary>
    ///     Danh sách phiên bản chức năng
    /// </summary>
    /// <returns></returns>
    [HttpGet("get-version-menu")]
    public async Task<JsonResponse<IEnumerable<ComboboxModalInt>>> V2023GetVersionMenu()
    {
        return
            new OkResponse<IEnumerable<ComboboxModalInt>>("", await _menuManagerAppService.V2023GetMenuVersion());
    }

#endregion
}