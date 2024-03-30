using System.IO;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ITC.Service.API.Controllers;

/// <summary>
///     Lấy file ghi log
/// </summary>
[Route("[controller]")]
[ApiController]
[Authorize]
public class LoggerController : ApiController
{
    private readonly IWebHostEnvironment _env;

    /// <summary>
    /// </summary>
    /// <param name="env"></param>
    /// <param name="notifications"></param>
    /// <param name="mediator"></param>
    public LoggerController(IWebHostEnvironment                      env,
                            INotificationHandler<DomainNotification> notifications,
                            IMediatorHandler                         mediator) : base(notifications, mediator)
    {
        _env = env;
    }

    /// <summary>
    ///     Lấy file ghi log
    /// </summary>
    /// <param name="dateLog">Ngày ghi log, định dạng yyyy-mm-dd. Nếu để trống sẽ lấy file log ngày hiện tại</param>
    /// <returns></returns>
    [HttpGet("log")]
    public IActionResult GetFileLog(string dateLog)
    {
        var nameLog                                 = "app.log";
        if (!string.IsNullOrEmpty(dateLog)) nameLog += "." + dateLog;
        var path                                    = _env.ContentRootPath + "/logs/" + nameLog;
        if (System.IO.File.Exists(path))
        {
            var fileStream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);

            return File(fileStream, "text/plain", nameLog);
        }

        return NotFound();
    }
}