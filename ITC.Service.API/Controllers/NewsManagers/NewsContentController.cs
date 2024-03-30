#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using ITC.Application.AppService.NewsManagers.NewsContentManagers;
using ITC.Application.Helpers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsContentManagers;
using ITC.Domain.Core.Notifications;
using ITC.Infra.CrossCutting.Identity.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NCore.Modals;
using NCore.Responses;
using NCore.Systems;
using static ITC.Application.AppService.NewsManagers.NewsContentManagers.NewsContentAppService;
using Tweetinvi;
using Tweetinvi.Models;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using OAuth;
using Tweetinvi.Security;
using HttpMethod = System.Net.Http.HttpMethod;

#endregion

namespace ITC.Service.API.Controllers.NewsManagers;

/// <summary>
///     Bài viết
/// </summary>
[Route("[controller]")]
[ApiController]
[Authorize]
public class NewsContentController : ApiController
{
#region Fields

    private readonly INewsContentAppService _newsContentAppService;

#endregion

#region Constructors

    /// <summary>
    ///     Hàm dựng
    /// </summary>
    /// <param name="newsContentAppService"></param>
    /// <param name="notifications"></param>
    /// <param name="mediator"></param>
    public NewsContentController(INewsContentAppService                   newsContentAppService,
                                 INotificationHandler<DomainNotification> notifications,
                                 IMediatorHandler                         mediator) :
        base(notifications, mediator)
    {
        _newsContentAppService = newsContentAppService;
    }

#endregion

#region Methods

    /// <summary>
    ///     Thêm mới Bài viết
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("create")]
    [CustomAuthorize(ModuleIdentity.NewsContent, TypeAudit.Add)]
    public async Task<IActionResult> Add([FromBody] NewsContentEventModel model)
    {
        if (ModelState.IsValid) return NResponseCommand(await _newsContentAppService.Add(model), model);
        NotifyModelStateErrors();
        return NResponseCommand(false, model);
    }

    /// <summary>
    ///     Sửa Bài viết
    /// </summary>
    /// <param name="model">Model menu</param>
    /// <returns></returns>
    [HttpPut("update")]
    [CustomAuthorize(ModuleIdentity.NewsContent, TypeAudit.Edit)]
    public async Task<IActionResult> Edit([FromBody] NewsContentEventModel model)
    {
        if (ModelState.IsValid) return NResponseCommand(await _newsContentAppService.Update(model), model);
        NotifyModelStateErrors();
        return NResponseCommand(false, model);

    }

    /// <summary>
    ///     Xóa Bài viết
    /// </summary>
    /// <param name="model">danh sách Id xóa</param>
    /// <returns></returns>
    [HttpPost("delete")]
    [CustomAuthorize(ModuleIdentity.NewsContent, TypeAudit.Delete)]
    public IActionResult Delete([FromBody] DeleteModal model)
    {
        return NResponseCommand(_newsContentAppService.Delete(model), model);
    }

    /// <summary>
    ///     Lấy Bài viết theo Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("get-by-id/{id:guid}")]
    [CustomAuthorize(ModuleIdentity.NewsContent, TypeAudit.View)]
    public async Task<IActionResult> GetById(Guid id)
    {
        return NResponseCommand(null, await _newsContentAppService.GetById(id));
    }

    /// <summary>
    ///     [Phân trang] Danh sách bài viết
    /// </summary>
    /// <param name="model">model dữ liệu nhận từ FE</param>
    /// <returns></returns>
    [CustomAuthorize(ModuleIdentity.NewsContent, TypeAudit.View)]
    [HttpGet("get-paging")]
    public async Task<JsonResponse<Pagination<NewsContentPagingDto>>> GetPaging(
        [FromQuery] NewsContentPagingModel model)
    {
        return await Task.Run(() =>
        {
            model.ModuleIdentity = ModuleIdentity.NewsContent;
            var lData = _newsContentAppService.GetPaging(model).Result.ToList();
            return new OkResponse<Pagination<NewsContentPagingDto>>(
                "",
                new Pagination<NewsContentPagingDto>
                {
                    PageLists = lData,
                    TotalRecord = lData.Count > 0
                                      ? lData[0].TotalRecord
                                      : 0
                });
        });
    }
    
    /// <summary>
    ///     [Phân trang] Danh sách bài viết đăng tự động
    /// </summary>
    /// <param name="model">model dữ liệu nhận từ FE</param>
    /// <returns></returns>
    [CustomAuthorize(ModuleIdentity.NewsContent, TypeAudit.View)]
    [HttpGet("get-paging-auto")]
    public async Task<JsonResponse<Pagination<NewsContentPagingDto>>> GetPagingAuto(
        [FromQuery] NewsContentPagingModel model)
    {
        return await Task.Run(() =>
        {
            model.ModuleIdentity = ModuleIdentity.NewsContent;
            var lData = _newsContentAppService.GetPagingAuto(model).Result.ToList();
            return new OkResponse<Pagination<NewsContentPagingDto>>(
                "",
                new Pagination<NewsContentPagingDto>
                {
                    PageLists = lData,
                    TotalRecord = lData.Count > 0
                                      ? lData[0].TotalRecord
                                      : 0
                });
        });
    }

    /// <summary>
    ///     [Combobox] NewsContentType
    /// </summary>
    /// <returns></returns>
    [HttpGet("get-combobox-news-content-type")]
    public async Task<JsonResponse<IEnumerable<ComboboxModalInt>>> GetComboboxNewsContentType()
    {
        return
            new OkResponse<IEnumerable<ComboboxModalInt>>(
                "", await _newsContentAppService.NewsContentTypeCombobox());
    }

    /// <summary>
    ///     [Combobox] Danh sách bài viết
    /// </summary>
    /// <returns></returns>
    [HttpGet("get-combobox-news-content")]
    public async Task<JsonResponse<Pagination<NewsContentPagingComboboxDto>>> GetComboboxNewsContent(
        [FromQuery] NewsContentPagingModel model)
    {
        return await Task.Run(() =>
        {
            var lData = _newsContentAppService.NewsContentCombobox(model).Result.ToList();
            return new OkResponse<Pagination<NewsContentPagingComboboxDto>>("",
                                                                            new Pagination<NewsContentPagingComboboxDto>
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
    ///     [Combobox] Danh sách tác giả
    /// </summary>
    /// <returns></returns>
    [HttpGet("news-author")]
    public async Task<JsonResponse<IEnumerable<ComboboxModal>>> NewsAuthor()
    {
        return
            new OkResponse<IEnumerable<ComboboxModal>>(
                "", await _newsContentAppService.NewsAuthor());
    }

    /// <summary>
    ///     [Combobox] Copy link
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("copy-link")]
    public async Task<JsonResponse<IEnumerable<ComboboxModal>>> CopyLink(Guid id)
    {
        return
            new OkResponse<IEnumerable<ComboboxModal>>("", await _newsContentAppService.CopyLink(id));
    }

    /// <summary>
    ///     Đọc dữ liệu từ link 
    /// </summary>
    /// <param name="id">Đường dẫn</param>
    /// <returns></returns>
    [HttpGet("read-link")]
    public async Task<JsonResponse<ReadLink>> ReadLink(string id)
    {
        return
            new OkResponse<ReadLink>("", await _newsContentAppService.ReadLink(id));
    }

    /// <summary>
    ///     Đăng bài lên facebook 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("post-news")]
    public async Task<JsonResponse<PostNewFaceError>> PostNews(PostNewFaceEvent model)
    {
        return
            new OkResponse<PostNewFaceError>("", await _newsContentAppService.PostNew(model));
    }
    
    /// <summary>
    ///     Cập nhật thời gian đăng bài tự động
    /// </summary>
    /// <param name="model">Model menu</param>
    /// <returns></returns>
    [HttpPost("update-time")]
    public async Task<IActionResult> UpdateTimePost([FromBody] NewsContentUpdateTimeAutoPostModel model)
    {
        if (ModelState.IsValid) return NResponseCommand(await _newsContentAppService.UpdateTimeAutoPost(model), model);
        NotifyModelStateErrors();
        return NResponseCommand(false, model);

    }

    /// <summary>
    ///     Lấy cấu hình lập lịch
    /// </summary>
    /// <returns></returns>
    [HttpGet("schedule-config")]
    public async Task<JsonResponse<int>> GetScheduleConfig()
    {
        return
            new OkResponse<int>("", await _newsContentAppService.GetScheduleConfig(ModuleIdentity.NewsContent));
    }
    
    /// <summary>
    ///     Lưu cấu hình lập lịch
    /// </summary>
    /// <returns></returns>
    [HttpGet("schedule-save")]
    public async Task<JsonResponse<int>> GetScheduleSave(int id)
    {
        return
            new OkResponse<int>("", await _newsContentAppService.GetScheduleSave(id));
    }
    #endregion
}