#region

#endregion

namespace ITC.Service.API.Helpers;

/// <summary>
/// </summary>
public class SitePagedListRenderOptions
{
#region Properties

    /// <summary>
    /// </summary>
    public static PagedListRenderOptions Boostrap4
    {
        get
        {
            var option = PagedListRenderOptions.Bootstrap4Full;

            option.MaximumPageNumbersToDisplay = 5;

            return option;
        }
    }

#endregion
}