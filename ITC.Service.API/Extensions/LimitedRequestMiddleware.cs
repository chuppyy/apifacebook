#region

using System.Collections.Generic;
using ITC.Application.Interfaces.ManageRole;
using ITC.Domain.Core.Bus;
using ITC.Domain.Interfaces;
using ITC.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RateLimitRequest.Middleware;
using RateLimitRequest.Models;
using RateLimitRequest.Store;

#endregion

namespace ITC.Service.API.Extensions;

/// <summary>
/// </summary>
public class LimitedRequestMiddleware : ClientRateLimitMiddleware
{
#region Constructors

    /// <summary>
    /// </summary>
    /// <param name="next"></param>
    /// <param name="options"></param>
    /// <param name="counterStore"></param>
    /// <param name="policyStore"></param>
    /// <param name="config"></param>
    /// <param name="logger"></param>
    public LimitedRequestMiddleware(RequestDelegate                    next
                                  , IOptions<ClientRateLimitOptions>   options
                                  , IRateLimitCounterStore             counterStore
                                  , IClientPolicyStore                 policyStore
                                  , IRateLimitConfiguration            config
                                  , ILogger<ClientRateLimitMiddleware> logger)
        : base(next, options, counterStore, policyStore, config, logger)
    {
        _next = next;
        // this.messageHub = messageHub;
    }

#endregion

#region Methods

    /// <summary>
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="rule"></param>
    /// <param name="retryAfter"></param>
    /// <param name="accountAppService"></param>
    /// <param name="user"></param>
    /// <param name="bus"></param>
    /// <param name="userManager"></param>
    public override void ReturnQuotaExceededResponse(HttpContext                  httpContext,
                                                     RateLimitRule                rule,
                                                     string                       retryAfter,
                                                     IAccountAppService           accountAppService,
                                                     IUser                        user,
                                                     IMediatorHandler             bus,
                                                     UserManager<ApplicationUser> userManager)
    {
        //return base.ReturnQuotaExceededResponse(httpContext, rule, retryAfter);
        var message = new { rule.Limit, rule.Period, retryAfter };

        httpContext.Response.Headers["Retry-After"] = retryAfter;

        httpContext.Response.StatusCode  = 429;
        httpContext.Response.ContentType = "application/json";
        //httpContext.Response.Redirect(RouteIdentity.AccountLogin, true);
        //return _next.Invoke(httpContext);
        if (rule.Users == null) rule.Users = new Dictionary<string, int>();
        if (rule.Users.ContainsKey(user.UserId) == false)
        {
            rule.Users.Add(user.UserId, 1);
        }
        else
        {
            rule.Users[user.UserId]++;
            if (rule.Users[user.UserId] > rule.TimesLock)
            {
                var currentUser = userManager.FindByIdAsync(user.UserId).Result;
                if (currentUser != null)
                {
                    var lockout = userManager.GetLockoutEndDateAsync(currentUser).Result;
                    if (lockout == null)
                    {
                        accountAppService.Lock(user.UserId);
                        httpContext.SignOutAsync(IdentityConstants.ExternalScheme);
                        //bus.RaiseEvent(new SystemLockedAccountEvent(StoredEventType.Update, Guid.Parse(user.UserId), string.Empty, Guid.Parse(user.UserId), user.FullName, Guid.Empty, user.PortalId));
                        rule.Users.Remove(user.UserId);
                        // messageHub.SignoutNotify(user.UserId);
                    }
                }
            }
        }

        httpContext.Response
                   .WriteAsync("Số lần thao tác vượt quá giới hạn cho phép. Vui lòng thử lại trong giây lát.");
    }

#endregion

#region Properties

    /// <summary>
    /// </summary>
    public IUser user { get; }

    private RequestDelegate _next { get; }

#endregion
}

//public class LimitedRequestMiddleware : IpRateLimitMiddleware
//{
//    IOptions<IpRateLimitOptions> _options;
//    public LimitedRequestMiddleware(RequestDelegate next
//        , IOptions<IpRateLimitOptions> options
//        , IRateLimitCounterStore counterStore
//        , IIpPolicyStore policyStore
//        , IRateLimitConfiguration config
//        , ILogger<IpRateLimitMiddleware> logger)
//            : base(next, options, counterStore, policyStore, config, logger)
//    {
//        _options = options;
//    }

//    public override Task ReturnQuotaExceededResponse(HttpContext httpContext, RateLimitRule rule, string retryAfter)
//    {
//        //return base.ReturnQuotaExceededResponse(httpContext, rule, retryAfter);
//        var message = new { rule.Limit, rule.Period, retryAfter };
//        httpContext.Response.Headers["Retry-After"] = retryAfter;

//        httpContext.Response.StatusCode = 200;
//        httpContext.Response.ContentType = "application/json";
//        return httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new JsonResult(new ResponseMessage(ResponseStage.ratelimit, "", RouteIdentity.RateLimit))));
//    }
//}