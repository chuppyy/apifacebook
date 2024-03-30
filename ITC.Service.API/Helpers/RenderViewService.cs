#region

using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

#endregion

namespace ITC.Service.API.Helpers;

/// <summary>
/// </summary>
public interface IViewRenderService
{
#region Methods

    /// <summary>
    /// </summary>
    /// <param name="viewName"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<string> RenderToStringAsync(string viewName,
                                     object model);

#endregion
}

/// <summary>
/// </summary>
public class ViewRenderService : IViewRenderService
{
#region Constructors

    /// <summary>
    /// </summary>
    /// <param name="razorViewEngine"></param>
    /// <param name="tempDataProvider"></param>
    /// <param name="serviceProvider"></param>
    public ViewRenderService(IRazorViewEngine  razorViewEngine,
                             ITempDataProvider tempDataProvider,
                             IServiceProvider  serviceProvider)
    {
        _razorViewEngine  = razorViewEngine;
        _tempDataProvider = tempDataProvider;
        _serviceProvider  = serviceProvider;
    }

#endregion

#region IViewRenderService Members

    /// <summary>
    /// </summary>
    /// <param name="viewName"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<string> RenderToStringAsync(string viewName, object model)
    {
        var httpContext   = new DefaultHttpContext { RequestServices = _serviceProvider };
        var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

        using (var sw = new StringWriter())
        {
            var viewResult = viewName.EndsWith(".cshtml")
                                 ? _razorViewEngine.GetView(viewName, viewName, false)
                                 : _razorViewEngine.FindView(actionContext, viewName, false);

            //var viewResult = _razorViewEngine.FindView(actionContext, viewName, false);
            if (viewResult.View == null)
                throw new ArgumentNullException($"{viewName} does not match any available view");

            var viewDictionary =
                new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = model
                };

            var viewContext = new ViewContext(
                actionContext,
                viewResult.View,
                viewDictionary,
                new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                sw,
                new HtmlHelperOptions()
            );

            await viewResult.View.RenderAsync(viewContext);
            return sw.ToString();
        }
    }

#endregion

#region Fields

    private readonly IRazorViewEngine  _razorViewEngine;
    private readonly IServiceProvider  _serviceProvider;
    private readonly ITempDataProvider _tempDataProvider;

#endregion
}