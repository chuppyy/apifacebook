#region

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RateLimitRequest.Core;
using RateLimitRequest.Models;
using RateLimitRequest.Store;

#endregion

namespace RateLimitRequest.Middleware;

public class ClientRateLimitMiddleware : RateLimitMiddleware<ClientRateLimitProcessor>
{
#region Fields

    private readonly ILogger<ClientRateLimitMiddleware> _logger;

#endregion

#region Constructors

    public ClientRateLimitMiddleware(RequestDelegate                    next,
                                     IOptions<ClientRateLimitOptions>   options,
                                     IRateLimitCounterStore             counterStore,
                                     IClientPolicyStore                 policyStore,
                                     IRateLimitConfiguration            config,
                                     ILogger<ClientRateLimitMiddleware> logger)
        : base(next, options?.Value,
               new ClientRateLimitProcessor(options?.Value, counterStore, policyStore, config), config)
    {
        _logger = logger;
    }

#endregion

#region Methods

    protected override void LogBlockedRequest(HttpContext      httpContext, ClientRequestIdentity identity,
                                              RateLimitCounter counter,     RateLimitRule         rule)
    {
        _logger.LogInformation(
            "Request {HttpVerb}:{Path} from ClientId {ClientId} has been blocked, quota {Limit}/{Period} exceeded by {Count}. Blocked by rule {Endpoint}, TraceIdentifier {TraceIdentifier}.",
            identity.HttpVerb, identity.Path, identity.ClientId, rule.Limit, rule.Period,
            counter.Count, rule.Endpoint, httpContext.TraceIdentifier);
    }

#endregion
}