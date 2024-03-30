#region

using System;

#endregion

namespace RateLimitRequest.Models;

/// <summary>
///     Stores the initial access time and the numbers of calls made from that point
/// </summary>
public struct RateLimitCounter
{
    public DateTime Timestamp { get; set; }

    public double Count { get; set; }
}