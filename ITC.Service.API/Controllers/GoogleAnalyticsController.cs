using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
        [HttpGet("report")]
        public async Task<JsonResponse<ReportSummary>> ReportGoogleAnalyticsAsync([FromQuery] GoogleAnalyticsReport query, CancellationToken cancellationToken)
        {
            return new OkResponse<ReportSummary>("", await _helperAppService.GoogleAnalyticsReportAsync(query, cancellationToken));
        }

        /// <summary>
        /// Báo cáo
        /// </summary>
        /// <returns></returns>
        [HttpGet("domin")]
        public async Task<JsonResponse<ResultXYDto>> GetDataFromStringAsync([FromQuery] GetDataFromStringQuery query, CancellationToken cancellationToken)
        {
            return new OkResponse<ResultXYDto>("", await _helperAppService.GetResultAsync(query, cancellationToken));
        }
    }
}
