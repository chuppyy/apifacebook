namespace ITC.Service.API.Models;

/// <summary>
/// </summary>
public class ErrorViewModel
{
#region Properties

    /// <summary>
    /// </summary>
    public string RequestId { get; set; }

    /// <summary>
    /// </summary>
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

#endregion
}