#region

using System;
using System.Threading.Tasks;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare;
using ITC.Domain.Core.Notifications;
using ITC.Infra.CrossCutting.Identity.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace ITC.Service.API.Controllers.Identitties;

/// <summary>
/// </summary>
[Route("[controller]")]
[ApiController]
[Authorize]
public class QueryDataController : ApiController
{
#region Fields

    private readonly IManageRoleQueries _manageRoleQueries;

#endregion

#region Constructors

    /// <summary>
    /// </summary>
    /// <param name="manageRoleQueries"></param>
    /// <param name="notifications"></param>
    /// <param name="mediator"></param>
    public QueryDataController(
        IManageRoleQueries                       manageRoleQueries,
        INotificationHandler<DomainNotification> notifications,
        IMediatorHandler                         mediator) : base(notifications, mediator)
    {
        _manageRoleQueries = manageRoleQueries;
    }

#endregion

#region Methods

    /// <summary>
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet("get")]
    public async Task<string> Get(QueryDataViewModel model)
    {
        try
        {
            return await _manageRoleQueries.Excecute(model.Query);
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

#endregion
}