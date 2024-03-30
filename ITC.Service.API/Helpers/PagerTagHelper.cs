#region

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;

#endregion

namespace ITC.Service.API.Helpers;

/// <summary>
/// </summary>
[HtmlTargetElement("pager")]
public class PagerTagHelper : TagHelper
{
#region Fields

    private readonly IUrlHelperFactory urlHelperFactory;

#endregion

#region Constructors

    /// <summary>
    /// </summary>
    /// <param name="urlHelperFactory"></param>
    public PagerTagHelper(IUrlHelperFactory urlHelperFactory)
    {
        this.urlHelperFactory = urlHelperFactory;
    }

#endregion

#region Static Fields and Constants

    private const string ListAttributeName = "list";

    private const string RouteValuesDictionaryName = "asp-all-route-data";

    private const string RouteValuesPrefix = "asp-route-";

    private const string ActionAttributeName = "asp-action";

    private const string ControllerAttributeName = "asp-controller";

    private const string AreaAttributeName = "asp-area";

    private const string OptionsAttributeName = "options";

    private const string ParamPageNumberAttributeName = "param-page-number";

#endregion

#region Properties

    /// <summary>
    /// </summary>
    [HtmlAttributeName(ActionAttributeName)]
    public string AspAction { get; set; }

    /// <summary>
    /// </summary>
    [HtmlAttributeName(AreaAttributeName)]
    public string AspArea { get; set; }

    /// <summary>
    /// </summary>
    [HtmlAttributeName(ControllerAttributeName)]
    public string AspController { get; set; }

    // [HtmlAttributeName(ListAttributeName)] public IPagedList List { get; set; }
    /// <summary>
    /// </summary>
    [HtmlAttributeName(OptionsAttributeName)]
    public PagedListRenderOptions Options { get; set; }

    /// <summary>
    /// </summary>
    [HtmlAttributeName(ParamPageNumberAttributeName)]
    public string ParamPageNumber { get; set; } = "page";

    /// <summary>
    /// </summary>
    [HtmlAttributeName(RouteValuesDictionaryName, DictionaryAttributePrefix = RouteValuesPrefix)]
    public IDictionary<string, string> RouteValues { get; set; } =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// </summary>
    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; }

#endregion

#region Methods

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    /// <param name="output"></param>
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        // if (List == null) return;
        //
        // if (Options == null) Options = PagedListRenderOptions.Bootstrap4PageNumbersPlusPrevAndNext;
        //
        // var urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
        // var listItemLinks = new List<TagBuilder>();
        //
        // //calculate start and end of range of page numbers
        // var firstPageToDisplay = 1;
        // var lastPageToDisplay = List.PageCount;
        // var pageNumbersToDisplay = lastPageToDisplay;
        //
        // if (Options.MaximumPageNumbersToDisplay.HasValue && List.PageCount > Options.MaximumPageNumbersToDisplay)
        // {
        //     // cannot fit all pages into pager
        //     var maxPageNumbersToDisplay = Options.MaximumPageNumbersToDisplay.Value;
        //
        //     firstPageToDisplay = List.PageNumber - maxPageNumbersToDisplay / 2;
        //
        //     if (firstPageToDisplay < 1) firstPageToDisplay = 1;
        //
        //     pageNumbersToDisplay = maxPageNumbersToDisplay;
        //     lastPageToDisplay    = firstPageToDisplay + pageNumbersToDisplay - 1;
        //
        //     if (lastPageToDisplay > List.PageCount)
        //     {
        //         lastPageToDisplay  = List.PageCount;
        //         firstPageToDisplay = List.PageCount - maxPageNumbersToDisplay + 1;
        //     }
        // }

        // //first
        // if (Options.DisplayLinkToFirstPage == PagedListDisplayMode.Always || Options.DisplayLinkToFirstPage == PagedListDisplayMode.IfNeeded && firstPageToDisplay > 1) listItemLinks.Add(First(urlHelper));
        //
        // //previous
        // if (Options.DisplayLinkToPreviousPage == PagedListDisplayMode.Always || Options.DisplayLinkToPreviousPage == PagedListDisplayMode.IfNeeded && !List.IsFirstPage) listItemLinks.Add(Previous(urlHelper));
        //
        // //text
        // if (Options.DisplayPageCountAndCurrentLocation) listItemLinks.Add(PageCountAndLocationText());
        //
        // //text
        // if (Options.DisplayItemSliceAndTotal) listItemLinks.Add(ItemSliceAndTotalText());
        //
        // //page
        // if (Options.DisplayLinkToIndividualPages)
        // {
        //     //if there are previous page numbers not displayed, show an ellipsis
        //     if (Options.DisplayEllipsesWhenNotShowingAllPageNumbers && firstPageToDisplay > 1) listItemLinks.Add(Ellipses());
        //
        //     for (var i = firstPageToDisplay; i <= lastPageToDisplay; i++)
        //     {
        //         //show delimiter between page numbers
        //         if (i > firstPageToDisplay && !string.IsNullOrWhiteSpace(Options.DelimiterBetweenPageNumbers)) listItemLinks.Add(WrapInListItem(Options.DelimiterBetweenPageNumbers));
        //
        //         //show page number link
        //         listItemLinks.Add(Page(i, urlHelper));
        //     }
        //
        //     //if there are subsequent page numbers not displayed, show an ellipsis
        //     if (Options.DisplayEllipsesWhenNotShowingAllPageNumbers && firstPageToDisplay + pageNumbersToDisplay - 1 < List.PageCount) listItemLinks.Add(Ellipses());
        // }
        //
        // //next
        // if (Options.DisplayLinkToNextPage == PagedListDisplayMode.Always || Options.DisplayLinkToNextPage == PagedListDisplayMode.IfNeeded && !List.IsLastPage) listItemLinks.Add(Next(urlHelper));
        //
        // //last
        // if (Options.DisplayLinkToLastPage == PagedListDisplayMode.Always || Options.DisplayLinkToLastPage == PagedListDisplayMode.IfNeeded && lastPageToDisplay < List.PageCount) listItemLinks.Add(Last(urlHelper));
        //
        // if (listItemLinks.Any())
        // {
        //     //append class to first item in list?
        //     if (!string.IsNullOrWhiteSpace(Options.ClassToApplyToFirstListItemInPager)) listItemLinks.First().AddCssClass(Options.ClassToApplyToFirstListItemInPager);
        //
        //     //append class to last item in list?
        //     if (!string.IsNullOrWhiteSpace(Options.ClassToApplyToLastListItemInPager)) listItemLinks.Last().AddCssClass(Options.ClassToApplyToLastListItemInPager);
        //
        //     //append classes to all list item links
        //     foreach (var li in listItemLinks)
        //     foreach (var c in Options.LiElementClasses ?? Enumerable.Empty<string>())
        //         li.AddCssClass(c);
        // }
        //
        // output.TagName = string.IsNullOrWhiteSpace(Options.ContainerHtmlTag) ? "div" : Options.ContainerHtmlTag;
        // output.TagMode = TagMode.StartTagAndEndTag;
        //
        // var ul = new TagBuilder("ul");
        //
        // foreach (var linkItem in listItemLinks) ul.InnerHtml.AppendHtml(linkItem);
        //
        // if (Options.UlElementClasses != null)
        //     foreach (var cssClass in Options.UlElementClasses)
        //         ul.AddCssClass(cssClass);
        //
        // output.Content.AppendHtml(ul);
    }

    private TagBuilder Ellipses()
    {
        var a = new TagBuilder("a");

        foreach (var @class in Options.AhrefElementClasses) a.AddCssClass(@class);

        a.InnerHtml.AppendHtml(Options.EllipsesFormat);

        return WrapInListItem(a, Options.DisabledElementClasses.ToArray());
    }

    private TagBuilder First(IUrlHelper urlHelper)
    {
        const int targetPageNumber = 1;
        var       first            = new TagBuilder("a");

        foreach (var @class in Options.AhrefElementClasses) first.AddCssClass(@class);

        first.InnerHtml.AppendHtml(string.Format(Options.LinkToFirstPageFormat, targetPageNumber));

        // if (List.IsFirstPage)
        // {
        //     first.Attributes["tabindex"] = "-1";
        //
        //     return WrapInListItem(first, Options.DisabledElementClasses.ToArray());
        // }
        //
        // first.Attributes["href"] = GeneratePageUrl(targetPageNumber, urlHelper);

        return WrapInListItem(first);
    }

    private string GeneratePageUrl(int pageNumber, IUrlHelper urlHelper)
    {
        var routeValues = new RouteValueDictionary(RouteValues);

        if (ParamPageNumber != null) routeValues[ParamPageNumber] = pageNumber;

        // Unconditionally replace any value from asp-route-area.
        if (AspArea != null) routeValues["area"] = AspArea;

        return urlHelper.Action(AspAction, AspController, routeValues);
    }

    private TagBuilder ItemSliceAndTotalText()
    {
        var text = new TagBuilder("a");

        //text.InnerHtml.AppendHtml(string.Format(Options.ItemSliceAndTotalFormat, List.FirstItemOnPage, List.LastItemOnPage, List.TotalItemCount));

        return WrapInListItem(text, Options.DisabledElementClasses.ToArray());
    }

    private TagBuilder Last(IUrlHelper urlHelper)
    {
        return null;
        // var targetPageNumber = List.PageCount;
        // var last = new TagBuilder("a");
        //
        // foreach (var @class in Options.AhrefElementClasses) last.AddCssClass(@class);
        //
        // last.InnerHtml.AppendHtml(string.Format(Options.LinkToLastPageFormat, targetPageNumber));
        //
        // if (List.IsLastPage) return WrapInListItem(last, Options.DisabledElementClasses.ToArray());
        //
        // last.Attributes["href"] = GeneratePageUrl(targetPageNumber, urlHelper);
        //
        // return WrapInListItem(last);
    }

    private TagBuilder Next(IUrlHelper urlHelper)
    {
        // var targetPageNumber = List.PageNumber + 1;
        // var next = new TagBuilder("a");
        //
        // foreach (var @class in Options.AhrefElementClasses) next.AddCssClass(@class);
        //
        // next.InnerHtml.AppendHtml(string.Format(Options.LinkToNextPageFormat, targetPageNumber));
        // next.Attributes["rel"] = "next";
        //
        // if (!List.HasNextPage)
        // {
        //     next.Attributes["tabindex"] = "-1";
        //
        //     return WrapInListItem(next, Options.DisabledElementClasses.ToArray());
        // }
        //
        // next.Attributes["href"] = GeneratePageUrl(targetPageNumber, urlHelper);
        //
        // return WrapInListItem(next);
        return null;
    }

    private TagBuilder Page(int i, IUrlHelper urlHelper)
    {
        // var targetPageNumber = i;
        // var isCurrentPage = targetPageNumber == List.PageNumber;
        //
        // var page = new TagBuilder(isCurrentPage ? "span" : "a");
        //
        // foreach (var @class in Options.AhrefElementClasses) page.AddCssClass(@class);
        //
        // page.InnerHtml.AppendHtml(string.Format(Options.LinkToIndividualPageFormat, targetPageNumber));
        //
        // if (targetPageNumber == List.PageNumber) return WrapInListItem(page, Options.ActiveElementClasses.ToArray());
        //
        // page.Attributes["href"] = GeneratePageUrl(targetPageNumber, urlHelper);
        //
        // return WrapInListItem(page);
        return null;
    }

    private TagBuilder PageCountAndLocationText()
    {
        // var text = new TagBuilder("a");
        //
        // text.InnerHtml.AppendHtml(string.Format(Options.PageCountAndCurrentLocationFormat, List.PageNumber, List.PageCount));
        //
        // return WrapInListItem(text, Options.DisabledElementClasses.ToArray());
        return null;
    }

    private TagBuilder Previous(IUrlHelper urlHelper)
    {
        // var targetPageNumber = List.PageNumber - 1;
        // var previous = new TagBuilder("a");
        //
        // foreach (var @class in Options.AhrefElementClasses) previous.AddCssClass(@class);
        //
        // previous.InnerHtml.AppendHtml(string.Format(Options.LinkToPreviousPageFormat, targetPageNumber));
        // previous.Attributes["rel"] = "prev";
        //
        // if (!List.HasPreviousPage)
        // {
        //     previous.Attributes["tabindex"] = "-1";
        //
        //     return WrapInListItem(previous, Options.DisabledElementClasses.ToArray());
        // }
        //
        // previous.Attributes["href"] = GeneratePageUrl(targetPageNumber, urlHelper);
        //
        // return WrapInListItem(previous);
        return null;
    }

    private TagBuilder WrapInListItem(string text)
    {
        var li = new TagBuilder("li");

        li.InnerHtml.AppendHtml(text);

        return li;
    }

    private TagBuilder WrapInListItem(TagBuilder inner, params string[] classes)
    {
        var li = new TagBuilder("li");

        if (classes != null)
            foreach (var @class in classes)
                li.AddCssClass(@class);

        li.InnerHtml.AppendHtml(inner);

        return li;
    }

#endregion
}