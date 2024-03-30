#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITC.Application.AppService.NewsManagers.NewsSeoKeyWordManagers;
using ITC.Application.Helpers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsSeoKeyWordManagers;
using ITC.Domain.Core.Notifications;
using ITC.Infra.CrossCutting.Identity.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NCore.Actions;
using NCore.Modals;
using NCore.Responses;
using NCore.Systems;

#endregion

namespace ITC.Service.API.Controllers.NewsManagers;

/// <summary>
///     Từ khóa SEO
/// </summary>
[Route("[controller]")]
[ApiController]
[Authorize]
public class NewsSeoKeyWordController : ApiController
{
#region Fields

    private readonly INewsSeoKeyWordAppService _newsSeoKeyWordAppService;

#endregion

#region Constructors

    /// <summary>
    ///     Hàm dựng
    /// </summary>
    /// <param name="newsSeoKeyWordAppService"></param>
    /// <param name="notifications"></param>
    /// <param name="mediator"></param>
    public NewsSeoKeyWordController(INewsSeoKeyWordAppService                newsSeoKeyWordAppService,
                                    INotificationHandler<DomainNotification> notifications,
                                    IMediatorHandler                         mediator) :
        base(notifications, mediator)
    {
        _newsSeoKeyWordAppService = newsSeoKeyWordAppService;
    }

#endregion

#region Methods

    /// <summary>
    ///     Thêm mới từ khóa SEO
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("create")]
    [CustomAuthorize(ModuleIdentity.NewsSeoKeyWord, TypeAudit.Add)]
    public IActionResult Add([FromBody] NewsSeoKeyWordEventModel model)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return NResponseCommand(false, model);
        }

        return NResponseCommand(_newsSeoKeyWordAppService.Add(model), model);
    }

    /// <summary>
    ///     Sửa từ khóa SEO
    /// </summary>
    /// <param name="model">Model menu</param>
    /// <returns></returns>
    [HttpPut("update")]
    [CustomAuthorize(ModuleIdentity.NewsSeoKeyWord, TypeAudit.Edit)]
    public IActionResult Edit([FromBody] NewsSeoKeyWordEventModel model)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return NResponseCommand(false, model);
        }

        return NResponseCommand(_newsSeoKeyWordAppService.Update(model), model);
    }

    /// <summary>
    ///     Xóa từ khóa SEO
    /// </summary>
    /// <param name="model">danh sách Id xóa</param>
    /// <returns></returns>
    [HttpPost("delete")]
    [CustomAuthorize(ModuleIdentity.NewsSeoKeyWord, TypeAudit.Delete)]
    public IActionResult Delete([FromBody] DeleteModal model)
    {
        return NResponseCommand(_newsSeoKeyWordAppService.Delete(model), model);
    }

    /// <summary>
    ///     Lấy từ khóa SEO theo Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("get-by-id/{id:guid}")]
    [CustomAuthorize(ModuleIdentity.NewsSeoKeyWord, TypeAudit.View)]
    public IActionResult GetById(Guid id)
    {
        return NResponseCommand(null, _newsSeoKeyWordAppService.GetById(id));
    }

    /// <summary>
    ///     [Phân trang] Danh sách từ khóa SEO
    /// </summary>
    /// <returns></returns>
    [CustomAuthorize(ModuleIdentity.NewsSeoKeyWord, TypeAudit.View)]
    [HttpGet("get-paging")]
    public async Task<JsonResponse<Pagination<NewsSeoKeyWordPagingDto>>> GetPaging([FromQuery] PagingModel model)
    {
        return await Task.Run(() =>
        {
            var lData = _newsSeoKeyWordAppService.GetPaging(new PagingModel
            {
                Search         = model.Search,
                PageNumber     = model.PageNumber,
                PageSize       = model.PageSize,
                StatusId       = model.StatusId,
                ModuleIdentity = ModuleIdentity.NewsSeoKeyWord,
                IsModal        = false
            }).Result.ToList();
            return new OkResponse<Pagination<NewsSeoKeyWordPagingDto>>("",
                                                                       new Pagination<NewsSeoKeyWordPagingDto>
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
    ///     [Phân trang] Danh sách từ khóa SEO
    /// </summary>
    /// <returns></returns>
    [HttpGet("get-paging-modal")]
    [CustomAuthorize(ModuleIdentity.Helpers, TypeAudit.ShowModal)]
    public async Task<JsonResponse<Pagination<NewsSeoKeyWordPagingDto>>> GetPagingModal(
        [FromQuery] PagingModel model)
    {
        return await Task.Run(() =>
        {
            var lData = _newsSeoKeyWordAppService.GetPaging(new PagingModel
            {
                Search         = model.Search,
                PageNumber     = model.PageNumber,
                PageSize       = model.PageSize,
                StatusId       = ActionStatusEnum.Active.Id,
                ModuleIdentity = "",
                IsModal        = true
            }).Result.ToList();
            return new OkResponse<Pagination<NewsSeoKeyWordPagingDto>>("",
                                                                       new Pagination<NewsSeoKeyWordPagingDto>
                                                                       {
                                                                           PageLists = lData,
                                                                           TotalRecord = lData.Count > 0
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
            new OkResponse<IEnumerable<ComboboxModal>>("", await _newsSeoKeyWordAppService.GetCombobox(search));
    }

#endregion
}