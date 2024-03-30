#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITC.Application.AppService.NewsManagers.NewsGithubManagers;
using ITC.Application.Helpers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsGithubManagers;
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
///     NewsGithub
/// </summary>
[Route("[controller]")]
[ApiController]
[Authorize]
public class NewsGithubController : ApiController
{
    #region Fields

    private readonly INewsGithubAppService _newsGithubAppService;

    #endregion

    #region Constructors

    /// <summary>
    ///     Hàm dựng
    /// </summary>
    /// <param name="newsGithubAppService"></param>
    /// <param name="notifications"></param>
    /// <param name="mediator"></param>
    public NewsGithubController(INewsGithubAppService                    newsGithubAppService,
                                INotificationHandler<DomainNotification> notifications,
                                IMediatorHandler                         mediator) :
        base(notifications, mediator)
    {
        _newsGithubAppService = newsGithubAppService;
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Thêm mới NewsGithub
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("create")]
    [CustomAuthorize(ModuleIdentity.NewsGithub, TypeAudit.Add)]
    public IActionResult Add([FromBody] NewsGithubEventModel model)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return NResponseCommand(false, model);
        }

        return NResponseCommand(_newsGithubAppService.Add(model), model);
    }

    /// <summary>
    ///     Sửa NewsGithub
    /// </summary>
    /// <param name="model">Model menu</param>
    /// <returns></returns>
    [HttpPut("update")]
    [CustomAuthorize(ModuleIdentity.NewsGithub, TypeAudit.Edit)]
    public IActionResult Edit([FromBody] NewsGithubEventModel model)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return NResponseCommand(false, model);
        }

        return NResponseCommand(_newsGithubAppService.Update(model), model);
    }

    /// <summary>
    ///     Xóa NewsGithub
    /// </summary>
    /// <param name="model">danh sách Id xóa</param>
    /// <returns></returns>
    [HttpPost("delete")]
    [CustomAuthorize(ModuleIdentity.NewsGithub, TypeAudit.Delete)]
    public IActionResult Delete([FromBody] DeleteModal model)
    {
        return NResponseCommand(_newsGithubAppService.Delete(model), model);
    }

    /// <summary>
    ///     Lấy NewsGithub theo Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("get-by-id/{id:guid}")]
    [CustomAuthorize(ModuleIdentity.NewsGithub, TypeAudit.View)]
    public IActionResult GetById(Guid id)
    {
        return NResponseCommand(null, _newsGithubAppService.GetById(id));
    }

    /// <summary>
    ///     [Phân trang] Danh sách NewsGithub
    /// </summary>
    /// <returns></returns>
    [CustomAuthorize(ModuleIdentity.NewsGithub, TypeAudit.View)]
    [HttpGet("get-paging")]
    public async Task<JsonResponse<Pagination<NewsGithubPagingDto>>> GetPaging([FromQuery] PagingModel model)
    {
        return await Task.Run(() =>
        {
            var lData = _newsGithubAppService.GetPaging(new PagingModel
            {
                Search         = model.Search,
                PageNumber     = model.PageNumber,
                PageSize       = model.PageSize,
                StatusId       = model.StatusId,
                ModuleIdentity = ModuleIdentity.NewsGithub
            }).Result.ToList();
            return new OkResponse<Pagination<NewsGithubPagingDto>>("",
                                                                   new Pagination<NewsGithubPagingDto>
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
            new OkResponse<IEnumerable<ComboboxModal>>("", await _newsGithubAppService.GetCombobox(search));
    }

    #endregion
}