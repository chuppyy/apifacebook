#region

using System;
using System.Collections.Generic;

#endregion

namespace RateLimitRequest.Models;

public class RateLimitRule
{
#region Properties

    /// <summary>
    ///     HTTP verb and path
    /// </summary>
    /// <example>
    ///     get:/api/values
    ///     *:/api/values
    ///     *
    /// </example>
    public string Endpoint { get; set; }

    /// <summary>
    ///     Maximum number of requests that a client can make in a defined period
    /// </summary>
    public double Limit { get; set; }

    /// <summary>
    ///     Rate limit period as in 1s, 1m, 1h
    /// </summary>
    public string Period { get; set; }

    public TimeSpan?               PeriodTimespan { get; set; }
    public int                     TimesLock      { get; set; }
    public int                     TimesTemp      { get; set; }
    public Dictionary<string, int> Users          { get; set; }

#endregion
}