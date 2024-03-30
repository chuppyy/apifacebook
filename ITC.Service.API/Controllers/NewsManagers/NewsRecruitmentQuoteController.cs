#region

using System;
using System.Linq;
using System.Threading.Tasks;
using ITC.Application.AppService.NewsManagers.NewsRecruitmentManagers;
using ITC.Application.Helpers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsRecruitmentManagers;
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

namespace ITC.Service.API.Controllers.NewsManagers;

/// <summary>
///     Báo giá
/// </summary>
[Route("[controller]")]
[ApiController]
[Authorize]
public class NewsRecruitmentQuoteController : ApiController
{
#region Fields

    private readonly INewsRecruitmentAppService _newsRecruitmentQuoteAppService;

#endregion

#region Constructors

    /// <summary>
    ///     Hàm dựng
    /// </summary>
    /// <param name="newsRecruitmentQuoteAppService"></param>
    /// <param name="notifications"></param>
    /// <param name="mediator"></param>
    public NewsRecruitmentQuoteController(INewsRecruitmentAppService               newsRecruitmentQuoteAppService,
                                          INotificationHandler<DomainNotification> notifications,
                                          IMediatorHandler                         mediator) :
        base(notifications, mediator)
    {
        _newsRecruitmentQuoteAppService = newsRecruitmentQuoteAppService;
    }

#endregion

#region Methods

    /// <summary>
    ///     Thêm mới Bài viết
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("create")]
    [CustomAuthorize(ModuleIdentity.NewsRecruitmentQuote, TypeAudit.Add)]
    public async Task<IActionResult> Add([FromBody] NewsRecruitmentEventModel model)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return NResponseCommand(false, model);
        }

        model.Type = NewsRecruitmentTableEnum.Quote.Id;
        return NResponseCommand(await _newsRecruitmentQuoteAppService.Add(model), model);
    }

    /// <summary>
    ///     Sửa Bài viết
    /// </summary>
    /// <param name="model">Model menu</param>
    /// <returns></returns>
    [HttpPut("update")]
    [CustomAuthorize(ModuleIdentity.NewsRecruitmentQuote, TypeAudit.Edit)]
    public async Task<IActionResult> Edit([FromBody] NewsRecruitmentEventModel model)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return NResponseCommand(false, model);
        }

        return NResponseCommand(await _newsRecruitmentQuoteAppService.Update(model), model);
    }

    /// <summary>
    ///     Xóa Bài viết
    /// </summary>
    /// <param name="model">danh sách Id xóa</param>
    /// <returns></returns>
    [HttpPost("delete")]
    [CustomAuthorize(ModuleIdentity.NewsRecruitmentQuote, TypeAudit.Delete)]
    public IActionResult Delete([FromBody] DeleteModal model)
    {
        return NResponseCommand(_newsRecruitmentQuoteAppService.Delete(model), model);
    }

    /// <summary>
    ///     Lấy Bài viết theo Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("get-by-id/{id:guid}")]
    [CustomAuthorize(ModuleIdentity.NewsRecruitmentQuote, TypeAudit.View)]
    public async Task<IActionResult> GetById(Guid id)
    {
        return NResponseCommand(null, await _newsRecruitmentQuoteAppService.GetById(id));
    }

    /// <summary>
    ///     [Phân trang] Danh sách bài viết
    /// </summary>
    /// <param name="model">model dữ liệu nhận từ FE</param>
    /// <returns></returns>
    [CustomAuthorize(ModuleIdentity.NewsRecruitmentQuote, TypeAudit.View)]
    [HttpGet("get-paging")]
    public async Task<JsonResponse<Pagination<NewsRecruitmentPagingDto>>> GetPaging(
        [FromQuery] PagingModel model)
    {
        return await Task.Run(() =>
        {
            model.ModuleIdentity = ModuleIdentity.NewsRecruitmentQuote;
            var lData =
                _newsRecruitmentQuoteAppService
                    .GetPaging(model, NewsRecruitmentTableEnum.Quote.Id)
                    .Result.ToList();
            return new OkResponse<Pagination<NewsRecruitmentPagingDto>>("",
                                                                        new Pagination<NewsRecruitmentPagingDto>
                                                                        {
                                                                            PageLists = lData,
                                                                            TotalRecord =
                                                                                lData.Count > 0
                                                                                    ? lData[0].TotalRecord
                                                                                    : 0
                                                                        });
        });
    }

#endregion
}