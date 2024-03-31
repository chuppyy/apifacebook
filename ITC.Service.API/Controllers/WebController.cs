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
    }
}
