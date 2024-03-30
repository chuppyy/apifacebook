#region

using System.Threading.Tasks;
using ITC.Domain.Interfaces;
using ITC.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

#endregion

namespace ITC.Service.API.Extensions;

/// <summary>
/// </summary>
public class UserActiveMiddleware
{
#region Fields

    private readonly RequestDelegate _next;

#endregion

#region Constructors

    /// <summary>
    /// </summary>
    /// <param name="next"></param>
    public UserActiveMiddleware(RequestDelegate next)
    {
        _next = next;
    }

#endregion

#region Methods

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    /// <param name="userManager"></param>
    /// <param name="currentUser"></param>
    /// <returns></returns>
    public async Task Invoke(HttpContext context, UserManager<ApplicationUser> userManager, IUser currentUser)
    {
        // string path = RouteIdentity.AccountNoneEmail;
        //// string addemailPath = RouteIdentity.AddEmailAccount;
        // string loginPath = RouteIdentity.AccountLogin;
        // string logoutPath = "/Identity/Account/Logout";
        // string[] extentions = { "js", "css","map","doc", "docx", "xls","xlsx","ppt","pptx","zip","rar","tar","gzip","gz","7z",
        // "txt","ini","avi","npg","nkv","mov","mp4","3gp","wmv","mp3","wav","jpg","jepg","png","gif","ico"};
        // if (context.Request?.Path != path && context.Request?.Path != addemailPath && context.Request?.Path != loginPath && context.Request?.Path != logoutPath)
        // {
        //     var strs = context.Request.Path.ToString().Split('.');
        //     if (!extentions.Contains(strs[strs.Length - 1]))
        //     {

        //         if (currentUser != null && !string.IsNullOrEmpty(currentUser.UserId) && string.IsNullOrEmpty(currentUser.Email))
        //         {
        //             var user = await userManager.FindByIdAsync(currentUser.UserId);
        //             if (user != null && string.IsNullOrEmpty(user.Email))
        //                 context.Response.Redirect(path, true);
        //         }
        //     }
        // }


        await _next.Invoke(context);
    }

#endregion
}

/// <summary>
/// </summary>
public static class MiddlewareExtentsion
{
#region Methods

    /// <summary>
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseUserActivition(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UserActiveMiddleware>();
    }

#endregion
}