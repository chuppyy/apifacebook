#region

using Microsoft.AspNetCore.Http;

#endregion

namespace RateLimitRequest.Models;

public class RateLimitHeaders
{
#region Properties

    public HttpContext Context { get; set; }

    public string Limit { get; set; }

    public string Remaining { get; set; }

    public string Reset { get; set; }

#endregion
}