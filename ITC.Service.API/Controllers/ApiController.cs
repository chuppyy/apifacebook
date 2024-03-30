#region

using System.Collections.Generic;
using System.Linq;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

#endregion

namespace ITC.Service.API.Controllers;

/// <summary>
/// </summary>
public abstract class ApiController : ControllerBase
{
#region Constructors

    /// <inheritdoc />
    protected ApiController(INotificationHandler<DomainNotification> notifications,
                            IMediatorHandler                         mediator)
    {
        _notifications = (DomainNotificationHandler)notifications;
        _mediator      = mediator;
    }

#endregion

#region Properties

    /// <summary>
    /// </summary>
    protected IEnumerable<DomainNotification> Notifications => _notifications.GetNotifications();

#endregion

#region Fields

    private readonly IMediatorHandler          _mediator;
    private readonly DomainNotificationHandler _notifications;

#endregion

#region Methods

    /// <summary>
    /// </summary>
    /// <param name="result"></param>
    protected void AddIdentityErrors(IdentityResult result)
    {
        foreach (var error in result.Errors) NotifyError(result.ToString(), error.Description);
    }

    /// <summary>
    /// </summary>
    /// <param name="modelState"></param>
    /// <returns></returns>
    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        var erros = ModelState.Values.SelectMany(v => v.Errors);
        foreach (var erro in erros)
        {
            var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
            NotifyError(string.Empty, erroMsg);
        }

        return CustomResponse();
    }


    /// <summary>
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    protected ActionResult CustomResponse(object result = null)
    {
        if (IsValidOperation())
            return Ok(new
            {
                success = true,
                data    = result
            });

        return BadRequest(new
        {
            success = false,
            errors  = _notifications.GetNotifications().Select(n => n.Value)
        });
    }

    /// <summary>
    ///     Trả về kết quả đăng nhập
    /// </summary>
    /// <param name="aToken"></param>
    /// <param name="rToken"></param>
    /// <param name="aUserFile"></param>
    /// <param name="rRoles"></param>
    /// <returns></returns>
    protected ActionResult LoginResponse(object aToken = null, object rToken = null, object aUserFile = null,
                                         object rRoles = null)
    {
        if (IsValidOperation())
            return Ok(new
            {
                success      = true,
                accesstoken  = aToken,
                refreshToken = rToken,
                userFile     = aUserFile,
                menuRoles    = rRoles
            });

        return BadRequest(new
        {
            success = false,
            errors  = _notifications.GetNotifications().Select(n => n.Value)
        });
    }

    /// <summary>
    ///     Trả về kết quả đăng nhập
    /// </summary>
    /// <param name="aToken"></param>
    /// <param name="rToken"></param>
    /// <param name="aUserFile"></param>
    /// <param name="rRoles"></param>
    /// <param name="isError"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    protected ActionResult LoginResponseV3(bool   isError,
                                           string message,
                                           object aToken    = null,
                                           object rToken    = null,
                                           object aUserFile = null,
                                           object rRoles    = null)
    {
        if (isError)
            return BadRequest(new
            {
                success = false,
                errors  = message
            });
        // var erros = ModelState.Values.SelectMany(v => v.Errors);
        // foreach (var erro in erros)
        // {
        //     var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
        //     NotifyError(string.Empty, erroMsg);
        // }
        if (IsValidOperation())
            return Ok(new
            {
                success      = true,
                accesstoken  = aToken,
                refreshToken = rToken,
                userFile     = aUserFile,
                menuRoles    = rRoles
            });

        return BadRequest(new
        {
            success = false,
            errors  = _notifications.GetNotifications().Select(n => n.Value)
        });
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    protected bool IsValidOperation()
    {
        return !_notifications.HasNotifications();
    }

    /// <summary>
    /// </summary>
    /// <param name="code"></param>
    /// <param name="message"></param>
    protected void NotifyError(string code, string message)
    {
        _mediator.RaiseEvent(new DomainNotification(code, message));
    }

    /// <summary>
    /// </summary>
    protected void NotifyModelStateErrors()
    {
        var erros = ModelState.Values.SelectMany(v => v.Errors);
        foreach (var erro in erros)
        {
            var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
            NotifyError(string.Empty, erroMsg);
        }
    }

    /// <summary>
    ///     Response
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    protected new IActionResult Response(object result = null)
    {
        if (IsValidOperation())
            return Ok(new
            {
                success = true,
                data    = result
            });

        return BadRequest(new
        {
            success = false,
            errors  = _notifications.GetNotifications().Select(n => n.Value)
        });
    }

    /// <summary>
    /// </summary>
    /// <param name="isResult">Giá trị đúng sai</param>
    /// <param name="result"></param>
    /// <returns></returns>
    protected IActionResult NResponseCommand(bool? isResult, object result = null)
    {
        if (isResult != null)
        {
            if (isResult == true)
                return Ok(new
                {
                    success = true,
                    code    = 200,
                    data    = result
                });
        }
        else
        {
            if (IsValidOperation())
                return Ok(new
                {
                    success = true,
                    code    = 200,
                    data    = result
                });
        }

        var iError = _notifications.GetNotifications().Select(n => n.Value).ToList();
        if (iError.Count == 0)
            return Ok(new
            {
                success = true,
                code    = 200,
                data    = result
            });
        return BadRequest(new
        {
            success = false,
            code    = 400,
            errors  = iError
        });
    }

    /// <summary>
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    protected IActionResult NResponse(object result = null)
    {
        if (IsValidOperation())
            return Ok(new
            {
                success = true,
                code    = 200,
                data    = result
            });

        return BadRequest(new
        {
            success = false,
            code    = 400,
            errors  = _notifications.GetNotifications().Select(n => n.Value)
        });
    }

    /// <summary>
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    protected IActionResult NResponseBadRequest(object result = null)
    {
        var lReturn = new List<string>
        {
            result?.ToString()
        };
        return BadRequest(new
        {
            success = false,
            code    = 400,
            errors  = lReturn
        });
    }

#endregion
}