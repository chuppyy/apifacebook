namespace ITC.Service.API.Configurations;

/// <summary>
///     AppSettings
/// </summary>
public class AppSettings
{
#region Properties

    /// <summary>
    ///     Expiration
    /// </summary>
    public int Expiration { get; set; }

    /// <summary>
    ///     Issuer
    /// </summary>
    public string Issuer { get; set; }

    /// <summary>
    ///     Secret
    /// </summary>
    public string Secret { get; set; }

    /// <summary>
    ///     ValidAt
    /// </summary>
    public string ValidAt { get; set; }

#endregion
}