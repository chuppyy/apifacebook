using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NCore.Responses;
using NCore.Systems;
using System.Threading.Tasks;
using ITC.Application.AppService.SystemManagers.HelperManagers;
using ITC.Domain.ResponseDto;
using System.Collections.Generic;
using ITC.Domain.Commands.GoogleAnalytics.Models;
using System.Threading;

namespace ITC.Service.API.Controllers
{
    /// <summary>
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class WebController : ApiController
    {

        private readonly IHelperAppService _helperAppService;

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="notifications"></param>
        /// <param name="mediator"></param>
        /// <param name="helperAppService"></param>
        public WebController(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediator, IHelperAppService helperAppService) : base(notifications, mediator)
        {
            _helperAppService = helperAppService;
        }

        /// <summary>
        /// Combobox trang web
        /// </summary>
        /// <returns></returns>
        [HttpGet("combobox")]
        public async Task<JsonResponse<List<ComboboxIdNameDto>>> GetComboboxAsync()
        {
            return new OkResponse<List<ComboboxIdNameDto>>("", await _helperAppService.GetComboboxAsync());
        }

        /// <summary>
        /// Tạo mail TM
        /// </summary>
        /// <returns></returns>
        [HttpPost("create-mail-tm")]
        public async Task<JsonResponse<bool>> CreateMailTMAsync(CreateMailTMCommand command, CancellationToken cancellationToken)
        {
            var result = await _helperAppService.CreateMailTMAsync(command, cancellationToken);
            if (result)
            {
                return new OkResponse<bool>("", result);
            }
            return new OkResponse<bool>("", result);
        }

        /// <summary>
        /// Lấy mã code mail TM
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-mail-tm")]
        public async Task<JsonResponse<string>> GetMailTMAsync([FromQuery] GetCodeMailTMQuery command, CancellationToken cancellationToken)
        {
            var result = await _helperAppService.GetCodeMailTMAsync(command, cancellationToken);
            return new OkResponse<string>("", result);
        }
    }
}
