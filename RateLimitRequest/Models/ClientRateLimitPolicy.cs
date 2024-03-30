namespace RateLimitRequest.Models;

public class ClientRateLimitPolicy : RateLimitPolicy
{
#region Properties

    public string ClientId { get; set; }

#endregion
}