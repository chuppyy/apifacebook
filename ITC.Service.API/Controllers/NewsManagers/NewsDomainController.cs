#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITC.Application.AppService.NewsManagers.NewsDomainManagers;
using ITC.Application.Helpers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsDomainManagers;
using ITC.Domain.Core.Notifications;
using ITC.Infra.CrossCutting.Identity.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NCore.Modals;
using NCore.Responses;
using NCore.Systems;

#endregion

namespace ITC.Service.API.Controllers.NewsManagers;

/// <summary>
///     loại nhóm tin
/// </summary>
[Route("[controller]")]
[ApiController]
[Authorize]
public class NewsDomainController : ApiController
{
    #region Fields

    private readonly INewsDomainAppService _newsDomainAppService;

    #endregion

    #region Constructors

    /// <summary>
    ///     Hàm dựng
    /// </summary>
    /// <param name="newsDomainAppService"></param>
    /// <param name="notifications"></param>
    /// <param name="mediator"></param>
    public NewsDomainController(INewsDomainAppService                    newsDomainAppService,
                                INotificationHandler<DomainNotification> notifications,
                                IMediatorHandler                         mediator) :
        base(notifications, mediator)
    {
        _newsDomainAppService = newsDomainAppService;
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Thêm mới loại nhóm tin
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("create")]
    [CustomAuthorize(ModuleIdentity.NewsDomain, TypeAudit.Add)]
    public IActionResult Add([FromBody] NewsDomainEventModel model)
    {
        if (ModelState.IsValid) return NResponseCommand(_newsDomainAppService.Add(model), model);
        NotifyModelStateErrors();
        return NResponseCommand(false, model);
    }

    /// <summary>
    ///     Sửa loại nhóm tin
    /// </summary>
    /// <param name="model">Model menu</param>
    /// <returns></returns>
    [HttpPut("update")]
    [CustomAuthorize(ModuleIdentity.NewsDomain, TypeAudit.Edit)]
    public IActionResult Edit([FromBody] NewsDomainEventModel model)
    {
        if (ModelState.IsValid) return NResponseCommand(_newsDomainAppService.Update(model), model);
        NotifyModelStateErrors();
        return NResponseCommand(false, model);
    }

    /// <summary>
    ///     Xóa loại nhóm tin
    /// </summary>
    /// <param name="model">danh sách Id xóa</param>
    /// <returns></returns>
    [HttpPost("delete")]
    [CustomAuthorize(ModuleIdentity.NewsDomain, TypeAudit.Delete)]
    public IActionResult Delete([FromBody] DeleteModal model)
    {
        return NResponseCommand(_newsDomainAppService.Delete(model), model);
    }

    /// <summary>
    ///     Lấy loại nhóm tin theo Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("get-by-id/{id:guid}")]
    [CustomAuthorize(ModuleIdentity.NewsDomain, TypeAudit.View)]
    public IActionResult GetById(Guid id)
    {
        return NResponseCommand(null, _newsDomainAppService.GetById(id));
    }

    /// <summary>
    ///     [Phân trang] Danh sách loại nhóm tin
    /// </summary>
    /// <returns></returns>
    [CustomAuthorize(ModuleIdentity.NewsDomain, TypeAudit.View)]
    [HttpGet("get-paging")]
    public async Task<JsonResponse<Pagination<NewsDomainPagingDto>>> GetPaging([FromQuery] PagingModel model)
    {
        return await Task.Run(() =>
        {
            var lData = _newsDomainAppService.GetPaging(new PagingModel
            {
                Search         = model.Search,
                PageNumber     = model.PageNumber,
                PageSize       = model.PageSize,
                StatusId       = model.StatusId,
                ModuleIdentity = ModuleIdentity.NewsDomain
            }).Result.ToList();
            return new OkResponse<Pagination<NewsDomainPagingDto>>(
                "",
                new Pagination<NewsDomainPagingDto>
                {
                    PageLists   = lData,
                    TotalRecord = lData.Count > 0 ? lData[0].TotalRecord : 0
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
            new OkResponse<IEnumerable<ComboboxModal>>("", await _newsDomainAppService.GetCombobox(search));
    }
    
    /// <summary>
    ///     Lấy cấu hình lập lịch
    /// </summary>
    /// <returns></returns>
    [HttpGet("schedule-config")]
    public async Task<JsonResponse<int>> GetScheduleConfig()
    {
        return
            new OkResponse<int>("", await _newsDomainAppService.GetScheduleConfig(ModuleIdentity.NewsDomain));
    }
    
    /// <summary>
    ///     Lưu cấu hình lập lịch
    /// </summary>
    /// <returns></returns>
    [HttpGet("schedule-save")]
    public async Task<JsonResponse<int>> GetScheduleSave(int id)
    {
        return
            new OkResponse<int>("", await _newsDomainAppService.GetScheduleSave(id));
    }

    #endregion
}