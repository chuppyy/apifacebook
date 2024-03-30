#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITC.Application.AppService.NewsManagers.NewsGroupTypeManagers;
using ITC.Application.Helpers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsGroupTypeManagers;
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
public class NewsGroupTypeController : ApiController
{
#region Fields

    private readonly INewsGroupTypeAppService _newsGroupTypeAppService;

#endregion

#region Constructors

    /// <summary>
    ///     Hàm dựng
    /// </summary>
    /// <param name="newsGroupTypeAppService"></param>
    /// <param name="notifications"></param>
    /// <param name="mediator"></param>
    public NewsGroupTypeController(INewsGroupTypeAppService                 newsGroupTypeAppService,
                                   INotificationHandler<DomainNotification> notifications,
                                   IMediatorHandler                         mediator) :
        base(notifications, mediator)
    {
        _newsGroupTypeAppService = newsGroupTypeAppService;
    }

#endregion

#region Methods

    /// <summary>
    ///     Thêm mới loại nhóm tin
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("create")]
    [CustomAuthorize(ModuleIdentity.NewsGroupType, TypeAudit.Add)]
    public IActionResult Add([FromBody] NewsGroupTypeEventModel model)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return NResponseCommand(false, model);
        }

        return NResponseCommand(_newsGroupTypeAppService.Add(model), model);
    }

    /// <summary>
    ///     Sửa loại nhóm tin
    /// </summary>
    /// <param name="model">Model menu</param>
    /// <returns></returns>
    [HttpPut("update")]
    [CustomAuthorize(ModuleIdentity.NewsGroupType, TypeAudit.Edit)]
    public IActionResult Edit([FromBody] NewsGroupTypeEventModel model)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return NResponseCommand(false, model);
        }

        return NResponseCommand(_newsGroupTypeAppService.Update(model), model);
    }

    /// <summary>
    ///     Xóa loại nhóm tin
    /// </summary>
    /// <param name="model">danh sách Id xóa</param>
    /// <returns></returns>
    [HttpPost("delete")]
    [CustomAuthorize(ModuleIdentity.NewsGroupType, TypeAudit.Delete)]
    public IActionResult Delete([FromBody] DeleteModal model)
    {
        return NResponseCommand(_newsGroupTypeAppService.Delete(model), model);
    }

    /// <summary>
    ///     Lấy loại nhóm tin theo Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("get-by-id/{id:guid}")]
    [CustomAuthorize(ModuleIdentity.NewsGroupType, TypeAudit.View)]
    public IActionResult GetById(Guid id)
    {
        return NResponseCommand(null, _newsGroupTypeAppService.GetById(id));
    }

    /// <summary>
    ///     [Phân trang] Danh sách loại nhóm tin
    /// </summary>
    /// <returns></returns>
    [CustomAuthorize(ModuleIdentity.NewsGroupType, TypeAudit.View)]
    [HttpGet("get-paging")]
    public async Task<JsonResponse<Pagination<NewsGroupTypePagingDto>>> GetPaging([FromQuery] PagingModel model)
    {
        return await Task.Run(() =>
        {
            var lData = _newsGroupTypeAppService.GetPaging(new PagingModel
            {
                Search         = model.Search,
                PageNumber     = model.PageNumber,
                PageSize       = model.PageSize,
                StatusId       = model.StatusId,
                ModuleIdentity = ModuleIdentity.NewsGroupType
            }).Result.ToList();
            return new OkResponse<Pagination<NewsGroupTypePagingDto>>("",
                                                                      new Pagination<NewsGroupTypePagingDto>
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
            new OkResponse<IEnumerable<ComboboxModal>>("", await _newsGroupTypeAppService.GetCombobox(search));
    }

#endregion
}