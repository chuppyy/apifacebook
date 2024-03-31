using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using NCore.Responses;
using System.Threading;
using ITC.Application.AppService.SystemManagers.HelperManagers;
using ITC.Domain.ResponseDto;
using NCore.Systems;
using ITC.Domain.Commands.GoogleAnalytics.Models;

namespace ITC.Service.API.Controllers
{
    /// <summary>
    /// Google Analytics
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class GoogleAnalyticsController : ApiController
    {
        private readonly IHelperAppService _helperAppService;

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="notifications"></param>
        /// <param name="mediator"></param>
        /// <param name="helperAppService"></param>
        public GoogleAnalyticsController(INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator, IHelperAppService helperAppService) : base(notifications, mediator)
        {
            _helperAppService = helperAppService;
        }
        /// <summary>
        /// Báo cáo
        /// </summary>
        /// <returns></returns>
        [HttpPost("report")]
        public async Task<JsonResponse<ReportGoogleAnalyticsDto>> ReportGoogleAnalyticsAsync([FromQuery] GoogleAnalyticsReport query, CancellationToken cancellationToken)
        {
            return new OkResponse<ReportGoogleAnalyticsDto>("", await _helperAppService.GoogleAnalyticsReportAsync(query, cancellationToken));
        }
    }
}
