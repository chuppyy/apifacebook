namespace RateLimitRequest.Models;

public class QuotaExceededResponse
{
#region Properties

    public string Content     { get; set; }
    public string ContentType { get; set; }

    public int? StatusCode { get; set; } = 429;

#endregion
}