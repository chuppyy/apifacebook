#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITC.Application.AppService.NewsManagers.NewsViaManagers;
using ITC.Application.Helpers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.StudyManagers.NewsVia;
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
///     NewsVia
/// </summary>
[Route("[controller]")]
[ApiController]
[Authorize]
public class NewsViaController : ApiController
{
#region Fields

    private readonly INewsViaAppService _newsViaAppService;

#endregion

#region Constructors

    /// <summary>
    ///     Hàm dựng
    /// </summary>
    /// <param name="newsViaAppService"></param>
    /// <param name="notifications"></param>
    /// <param name="mediator"></param>
    public NewsViaController(INewsViaAppService                       newsViaAppService,
                             INotificationHandler<DomainNotification> notifications,
                             IMediatorHandler                         mediator) :
        base(notifications, mediator)
    {
        _newsViaAppService = newsViaAppService;
    }

#endregion

#region Methods

    /// <summary>
    ///     Thêm mới NewsVia
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("create")]
    [CustomAuthorize(ModuleIdentity.NewsVia, TypeAudit.Add)]
    public IActionResult Add([FromBody] NewsViaEventModel model)
    {
        if (ModelState.IsValid) return NResponseCommand(_newsViaAppService.Add(model), model);
        NotifyModelStateErrors();
        return NResponseCommand(false, model);
    }

    /// <summary>
    ///     Sửa NewsVia
    /// </summary>
    /// <param name="model">Model menu</param>
    /// <returns></returns>
    [HttpPut("update")]
    [CustomAuthorize(ModuleIdentity.NewsVia, TypeAudit.Edit)]
    public IActionResult Edit([FromBody] NewsViaEventModel model)
    {
        if (ModelState.IsValid) return NResponseCommand(_newsViaAppService.Update(model), model);
        NotifyModelStateErrors();
        return NResponseCommand(false, model);

    }

    /// <summary>
    ///     Xóa NewsVia
    /// </summary>
    /// <param name="model">danh sách Id xóa</param>
    /// <returns></returns>
    [HttpPost("delete")]
    [CustomAuthorize(ModuleIdentity.NewsVia, TypeAudit.Delete)]
    public IActionResult Delete([FromBody] DeleteModal model)
    {
        return NResponseCommand(_newsViaAppService.Delete(model), model);
    }

    /// <summary>
    ///     Lấy NewsVia theo Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("get-by-id/{id:guid}")]
    [CustomAuthorize(ModuleIdentity.NewsVia, TypeAudit.View)]
    public IActionResult GetById(Guid id)
    {
        return NResponseCommand(null, _newsViaAppService.GetById(id));
    }

    /// <summary>
    ///     [Phân trang] Danh sách NewsVia
    /// </summary>
    /// <returns></returns>
    [CustomAuthorize(ModuleIdentity.NewsVia, TypeAudit.View)]
    [HttpGet("get-paging")]
    public async Task<JsonResponse<Pagination<NewsViaPagingDto>>> GetPaging([FromQuery] PagingModel model)
    {
        return await Task.Run(() =>
        {
            var lData = _newsViaAppService.GetPaging(new PagingModel
            {
                Search         = model.Search,
                PageNumber     = model.PageNumber,
                PageSize       = model.PageSize,
                StatusId       = model.StatusId,
                ModuleIdentity = ModuleIdentity.NewsVia
            }).Result.ToList();
            return new OkResponse<Pagination<NewsViaPagingDto>>(
                "",
                new Pagination<NewsViaPagingDto>
                {
                    PageLists = lData,
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
            new OkResponse<IEnumerable<ComboboxModal>>("", await _newsViaAppService.GetCombobox(search));
    }

#endregion
}