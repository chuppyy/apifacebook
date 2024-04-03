using ITC.Application.AppService.SystemManagers.HelperManagers;
using ITC.Domain.Commands.GoogleAnalytics.Models;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Notifications;
using ITC.Domain.ResponseDto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NCore.Responses;
using NCore.Systems;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITC.Service.API.Controllers
{
    /// <summary>
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class ReportController : ApiController
    {

        private readonly IHelperAppService _helperAppService;

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="notifications"></param>
        /// <param name="mediator"></param>
        /// <param name="helperAppService"></param>
        public ReportController(INotificationHandler<DomainNotification> notifications, 
            IMediatorHandler mediator, IHelperAppService helperAppService) : base(notifications, mediator)
        {
            _helperAppService = helperAppService;
        }

        /// <summary>
        /// báo cáo tổng quan user
        /// </summary>
        /// <returns></returns>
        [HttpGet("user")]
        public async Task<JsonResponse<List<ReportUserGroupResponseDto>>> ReportUserGroupNewAsync([FromQuery] ReportUserPostQuery query)
        {
            return new OkResponse<List<ReportUserGroupResponseDto>>("", await _helperAppService.ReportUserGroupNewAsync(query));
        }
    }
}
