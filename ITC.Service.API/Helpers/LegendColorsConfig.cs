namespace ITC.Service.API.Helpers;

/// <summary>
/// </summary>
public class LegendColor
{
#region Properties

    /// <summary>
    /// </summary>
    public string Class { get; set; }

    /// <summary>
    /// </summary>
    public string Color { get; set; }

#endregion
}

/// <summary>
/// </summary>
public class LegendColorsConfig
{
#region Properties

    /// <summary>
    /// </summary>
    public LegendColor Blue { get; set; }

    /// <summary>
    /// </summary>
    public LegendColor Cyan { get; set; }

    /// <summary>
    /// </summary>
    public LegendColor Green { get; set; }

    /// <summary>
    /// </summary>
    public LegendColor Grey { get; set; }

    /// <summary>
    /// </summary>
    public LegendColor Pink { get; set; }

    /// <summary>
    /// </summary>
    public LegendColor Purple { get; set; }

    /// <summary>
    /// </summary>
    public LegendColor Red { get; set; }

    /// <summary>
    /// </summary>
    public LegendColor Yellow { get; set; }

#endregion
}