namespace RateLimitRequest.Models;

public class IpRateLimitPolicy : RateLimitPolicy
{
#region Properties

    public string Ip { get; set; }

#endregion
}