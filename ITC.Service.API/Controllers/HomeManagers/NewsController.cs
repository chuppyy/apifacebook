#region

using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Application.AppService.NewsManagers.NewsContentManagers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.HomeManager;
using ITC.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NCore.Responses;
using NCore.Systems;

#endregion

namespace ITC.Service.API.Controllers.HomeManagers;

/// <inheritdoc />
[Route("[controller]")]
[ApiController]
public class NewsController : ApiController
{
    private readonly INewsContentAppService _newsContentAppService;

    #region Constructors

    /// <inheritdoc />
    public NewsController(INewsContentAppService                   newsContentAppService,
                          INotificationHandler<DomainNotification> notifications,
                          IMediatorHandler                         mediator) : base(notifications, mediator)
    {
        _newsContentAppService = newsContentAppService;
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Dữ liệu chi tiết bài viết
    /// </summary>
    /// <returns></returns>
    [HttpGet("news-detail")]
    public async Task<JsonResponse<NewsMainModel>> GetNewsDetail(string id)
    {
        return
            new OkResponse<NewsMainModel>("", await _newsContentAppService.GetDetail(id));
    }

    /// <summary>
    ///     Dữ liệu chi tiết bài viết
    /// </summary>
    /// <returns></returns>
    [HttpGet("news-detailnew")]
    public async Task<JsonResponse<NewsMainModel>> GetNewsDetailNew(string id)
    {
        return
            new OkResponse<NewsMainModel>("", await _newsContentAppService.GetDetailNew(id));
    }

    /// <summary>
    ///     Dữ liệu chi tiết bài viết
    /// </summary>
    /// <returns></returns>
    [HttpGet("news-detailbasic")]
    public async Task<JsonResponse<NewsMainModel>> GetNewsDetailBasic(string id)
    {
        return
            new OkResponse<NewsMainModel>("", await _newsContentAppService.GetDetailBasic(id));
    }


    /// <summary>
    ///     Danh sách bài viết
    /// </summary>
    /// <returns></returns>
    [HttpGet("news-list")]
    public async Task<JsonResponse<IEnumerable<HomeMainGroupModel>>> ListNews([FromQuery] NewsGroupMainEvent model)
    {
        return
            new OkResponse<IEnumerable<HomeMainGroupModel>>("", await _newsContentAppService.ListContentByGroup(model.Group, model.Number));
    }

    /// <summary>
    ///     Danh sách bài viết từ cơ sở dữ liệu khác
    /// </summary>
    /// <returns></returns>
    [HttpGet("news-life")]
    public async Task<JsonResponse<HomeNewsLifeModel>> NewsLife(string id)
    {
        return
            new OkResponse<HomeNewsLifeModel>("", await _newsContentAppService.HomeNewsLifeModel(id));
    }
    #endregion
}