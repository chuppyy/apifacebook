namespace RateLimitRequest.Models;

/// <summary>
///     Stores the client IP, ID, endpoint and verb
/// </summary>
public class ClientRequestIdentity
{
#region Properties

    public string ClientId { get; set; }
    public string ClientIp { get; set; }

    public string HttpVerb { get; set; }

    public string Path { get; set; }

#endregion
}