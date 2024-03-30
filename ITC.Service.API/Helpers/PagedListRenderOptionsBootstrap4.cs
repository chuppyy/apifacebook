namespace ITC.Service.API.Helpers;

/// <summary>
/// </summary>
public partial class PagedListRenderOptions
{
#region Methods

    private static void SetBootstrap4Option(PagedListRenderOptions option)
    {
        option.ContainerHtmlTag         = DefaultContainerHtmlTag;
        option.UlElementClasses         = DefaultUlElementClasses;
        option.LiElementClasses         = DefaultLiElementClasses;
        option.AhrefElementClasses      = DefaultAhrefElementClasses;
        option.LinkToPreviousPageFormat = DefaultLinkToPreviousPageFormat;
        option.LinkToNextPageFormat     = DefaultLinkToNextPageFormat;
        option.LinkToFirstPageFormat    = DefaultLinkToFirstPageFormat;
        option.LinkToLastPageFormat     = DefaultLinkToLastPageFormat;
    }

#endregion

#region Static Fields and Constants

    private const string DefaultContainerHtmlTag = "nav";

    private const string DefaultLinkToPreviousPageFormat = "Trước";

    private const string DefaultLinkToNextPageFormat = "Sau";

    private const string DefaultLinkToFirstPageFormat = "Đầu tiên";

    private const string DefaultLinkToLastPageFormat = "Cuối cùng";

    private static readonly string[] DefaultUlElementClasses = { "pagination" };

    private static readonly string[] DefaultLiElementClasses = { "page-item" };

    private static readonly string[] DefaultAhrefElementClasses = { "page-link" };

#endregion

#region Properties

    /// <summary>
    ///     Show Page Numbers plus Previous, Next, First and Last links.
    /// </summary>
    public static PagedListRenderOptions Bootstrap4Full
    {
        get
        {
            var option = new PagedListRenderOptions();

            SetBootstrap4Option(option);
            SetFullOption(option);

            return option;
        }
    }

    /// <summary>
    ///     Only show Previous and Next links.
    /// </summary>
    public static PagedListRenderOptions Bootstrap4Minimal
    {
        get
        {
            var option = new PagedListRenderOptions();

            SetBootstrap4Option(option);
            SetMinimalOption(option);

            return option;
        }
    }

    /// <summary>
    ///     Only show Page Numbers.
    /// </summary>
    public static PagedListRenderOptions Bootstrap4PageNumbersOnly
    {
        get
        {
            var option = new PagedListRenderOptions();

            SetBootstrap4Option(option);
            SetPageNumbersOnlyOption(option);

            return option;
        }
    }

    /// <summary>
    ///     Show Page Numbers plus First and Last links.
    /// </summary>
    public static PagedListRenderOptions Bootstrap4PageNumbersPlusFirstAndLast
    {
        get
        {
            var option = new PagedListRenderOptions();

            SetBootstrap4Option(option);
            SetPageNumbersPlusFirstAndLastOption(option);

            return option;
        }
    }

    /// <summary>
    ///     Show Page Numbers plus Previous and Next links.
    /// </summary>
    public static PagedListRenderOptions Bootstrap4PageNumbersPlusPrevAndNext
    {
        get
        {
            var option = new PagedListRenderOptions();

            SetBootstrap4Option(option);
            SetPageNumbersPlusPrevAndNextOption(option);

            return option;
        }
    }

#endregion
}