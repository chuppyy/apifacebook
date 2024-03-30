#region

using System.Threading.Tasks;
using ITC.Application.ViewModels.ManageRole;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Events;
using ITC.Domain.Core.Notifications;
using ITC.Domain.Interfaces;
using ITC.Infra.CrossCutting.Identity.Extensions;
using ITC.Infra.CrossCutting.Identity.Interfaces;
using ITC.Infra.CrossCutting.Identity.Models;
using ITC.Service.API.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace ITC.Service.API.Controllers;

/// <summary>
///     Tài khoản
/// </summary>
[Route("[controller]")]
[ApiController]
[Authorize]
public class AccountController : ApiController
{
#region Constructors

    /// <summary>
    /// </summary>
    /// <param name="userManager"></param>
    /// <param name="accountRepository"></param>
    /// <param name="user"></param>
    /// <param name="notifications"></param>
    /// <param name="mediator"></param>
    public AccountController(CustomUserManager<ApplicationUser>       userManager,
                             IAccountRepository                       accountRepository,
                             IUser                                    user,
                             INotificationHandler<DomainNotification> notifications,
                             IMediatorHandler                         mediator) : base(notifications, mediator)
    {
        _userManager       = userManager;
        _user              = user;
        _accountRepository = accountRepository;
    }

#endregion

#region Fields

    private readonly IAccountRepository                 _accountRepository;
    private readonly IUser                              _user;
    private readonly CustomUserManager<ApplicationUser> _userManager;

#endregion

#region Methods

    /// <summary>
    ///     Lấy số bản ghi tài khoản của hệ thống
    /// </summary>
    /// <returns></returns>
    [HttpGet("add-session")]
    public bool Addsession()
    {
        var vl = new SesstionValue
        {
            UnitName = _user.FullName,
            UserId   = _user.UserId
        };
        SesstionValue.SetValue(HttpContext.Session, vl);
        return true;
    }

    /// <summary>
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangPassword(ChangePasswordAccountViewModel model)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var applicationUser = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if (model.NewPassword.Length < 6)
        {
            ModelState.AddModelError(string.Empty, "Mật khẩu quá ngắn.");
            return CustomResponse(ModelState);
        }

        var result =
            await _accountRepository.ChangePasswordAsync(applicationUser, model.OldPassword, model.NewPassword);
        if (result.Succeeded) return CustomResponse();

        ModelState.AddModelError(string.Empty, "lỗi cập nhật");
        return CustomResponse(ModelState);
    }

#endregion
}

/// <summary>
///     LoginStoredEvent
/// </summary>
public class LoginStoredEvent : StoredEvent
{
#region Constructors

    /// <summary>
    ///     LoginStoredEvent
    /// </summary>
    /// <param name="user"></param>
    /// <param name="unitUserId"></param>
    /// <param name="ipAddress"></param>
    public LoginStoredEvent(ApplicationUser user, string unitUserId, string ipAddress) :
        base(StoredEventType.Access, user.Id, user.FullName,
             ipAddress, ipAddress, unitUserId, user.PortalId)
    {
    }

#endregion

#region Methods

    // private static Guid IpToGuid(string ip)
    // {
    //     if (!string.IsNullOrEmpty(ip) && ValidateIPv4(ip))
    //     {
    //         var str = "";
    //         ip.Split('.').ForEach(x =>
    //         {
    //             for (var i = x.Length; i < 3; i++) str += "0";
    //             str += x;
    //         });
    //         return new Guid("00000000-0000-0000-0000-" + str);
    //     }
    //
    //     return Guid.Empty;
    // }

    // /// <summary>
    // ///     Kiểm tra địa chỉ IP đúng chuẩn
    // /// </summary>
    // /// <param name="ipString"></param>
    // /// <returns></returns>
    // private static bool ValidateIPv4(string ipString)
    // {
    //     if (ipString.Count(c => c == '.') != 3) return false;
    //
    //     IPAddress address;
    //     return IPAddress.TryParse(ipString, out address);
    // }

#endregion
}