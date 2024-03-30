#region

using System;
using System.Collections.Generic;
using ITC.Application.Helpers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace ITC.Service.API.Controllers;

/// <summary>
/// </summary>
[Route("[controller]")]
[ApiController]
public class GeneralController : ApiController
{
#region Constructors

    /// <summary>
    /// </summary>
    /// <param name="notifications"></param>
    /// <param name="mediator"></param>
    public GeneralController(
        INotificationHandler<DomainNotification> notifications,
        IMediatorHandler                         mediator) : base(notifications, mediator)
    {
    }

#endregion

#region Methods

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [HttpGet(GeneralApi.GetYear)]
    public List<int> GetYears()
    {
        var years       = new List<int>();
        var currentYear = DateTime.UtcNow.Year;
        //years.Add(currentYear + 1);
        var amount = 100;
        for (var i = currentYear; i > currentYear - amount; i--) years.Add(i);
        return years;
    }

#endregion
}