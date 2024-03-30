#region

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ITC.Application.AppService.AuthorityManager.MenuManagerSystem;
using ITC.Application.AppService.CompanyManagers.StaffManagers;
using ITC.Application.Interfaces.ManageRole;
using ITC.Application.ViewModels.Account;
using ITC.Application.ViewModels.ManageRole;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Notifications;
using ITC.Infra.CrossCutting.Identity.Extensions;
using ITC.Infra.CrossCutting.Identity.Interfaces;
using ITC.Infra.CrossCutting.Identity.Models;
using ITC.Infra.CrossCutting.Identity.Queries;
using ITC.Service.API.Configurations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

#endregion

namespace ITC.Service.API.Controllers;

/// <inheritdoc />
[Route("[controller]")]
[ApiController]
public class LoginController : ApiController
{
#region Constructors

    /// <inheritdoc />
    public LoginController(CustomUserManager<ApplicationUser>       userManager,
                           SignInManager<ApplicationUser>           signInManager,
                           IManageRoleQueries                       manageRoleQueries,
                           IAccountRepository                       accountRepository,
                           ITokenAppService                         tokenAppService,
                           IMenuManagerAppService                   menuManagerAppService,
                           IStaffManagerAppService                  staffManagerAppService,
                           IOptions<AppSettings>                    appSettings,
                           INotificationHandler<DomainNotification> notifications,
                           IMediatorHandler                         mediator) : base(notifications, mediator)
    {
        _userManager            = userManager;
        _signInManager          = signInManager;
        _manageRoleQueries      = manageRoleQueries;
        _appSettings            = appSettings.Value;
        _accountRepository      = accountRepository;
        _tokenAppService        = tokenAppService;
        _menuManagerAppService  = menuManagerAppService;
        _staffManagerAppService = staffManagerAppService;
    }

#endregion

#region Fields

    private readonly IAccountRepository                 _accountRepository;
    private readonly AppSettings                        _appSettings;
    private readonly IManageRoleQueries                 _manageRoleQueries;
    private readonly SignInManager<ApplicationUser>     _signInManager;
    private readonly ITokenAppService                   _tokenAppService;
    private readonly IMenuManagerAppService             _menuManagerAppService;
    private readonly IStaffManagerAppService            _staffManagerAppService;
    private readonly CustomUserManager<ApplicationUser> _userManager;

#endregion

#region Methods

    /// <summary>
    ///     Đăng nhập
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        // if (string.IsNullOrEmpty(model.UrlPage))
        // {
        //     ModelState.AddModelError(string.Empty, "Website không tồn tại trong hệ thống.");
        //     return CustomResponse(ModelState);
        // }

        var iUserName = model.UserName;
        var user      = await _userManager.FindByNameOrEmailAsync(iUserName);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Tài khoản không tồn tại.");
            return CustomResponse(ModelState);
        }

        // if (!user.EmailConfirmed)
        // {
        //     ModelState.AddModelError(string.Empty, "Tài khoản chưa được kích hoạt.");
        //
        //     return CustomResponse(ModelState);
        // }

        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);
        if (result.Succeeded)
        {
            var accessToken  = await GenerateJwt(user.Id);
            var refreshToken = _tokenAppService.GenerateRefreshToken();
            user.RefreshToken           = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(1);
            _accountRepository.Update(user);
            _accountRepository.SaveChanges();

            // Lấy danh sách các quyền sử dụng
            var iRole = _menuManagerAppService.GetMenu(user.Id).Result.ToList();
            // Lấy ảnh đại diện
            return LoginResponse(accessToken, refreshToken, new aUserFile
            {
                UserId   = user.Id,
                FullName = user.FullName,
                Avatar   = _staffManagerAppService.AvatarLink(user.Id)
            }, iRole);
        }

        if (result.IsLockedOut)
            ModelState.AddModelError("AccountLocked", "Tài khoản đã bị khóa");
        else
            ModelState.AddModelError("PasswordInvalid", "Mật khẩu không chính xác");
        return CustomResponse(ModelState);
    }

    /// <summary>
    /// </summary>
    /// <param name="tokenApiModel"></param>
    /// <returns></returns>
    [HttpPost("refresh-token")]
    public async Task<OkObjectResult> RefreshToken(TokenViewModel tokenApiModel)
    {
        if (tokenApiModel is null)
            return Ok(new
            {
                Code         = 400,
                accessToken  = "",
                refreshToken = ""
            });

        var accessToken  = tokenApiModel.AccessToken.Replace("Bearer ", "");
        var refreshToken = tokenApiModel.RefreshToken;

        if (string.IsNullOrEmpty(accessToken))
            return Ok(new
            {
                Code         = 400,
                accessToken  = "",
                refreshToken = ""
            });

        if (string.IsNullOrEmpty(refreshToken))
            return Ok(new
            {
                Code         = 400,
                accessToken  = "",
                refreshToken = ""
            });

        if (accessToken == "undefined")
            return Ok(new
            {
                Code         = 400,
                accessToken  = "",
                refreshToken = ""
            });

        var key       = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var principal = _tokenAppService.GetPrincipalFromExpiredToken(accessToken, key);
        var username = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)
                                ?.Value; //this is mapped to the Name claim by default
        var user = await _userManager.FindByIdAsync(username);
        if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            return Ok(new
            {
                Code         = 400,
                accessToken  = "",
                refreshToken = ""
            });

        var newAccessToken  = await GenerateJwt(user.Id);
        var newRefreshToken = _tokenAppService.GenerateRefreshToken();
        user.RefreshToken = newRefreshToken;
        _accountRepository.Update(user);
        _accountRepository.SaveChanges();
        return Ok(new
        {
            Code         = 200,
            accessToken  = newAccessToken,
            refreshToken = newRefreshToken
        });
    }

    /// <summary>
    ///     Đăng xuất hệ thống
    /// </summary>
    /// <returns></returns>
    [HttpPost("log-out")]
    public async Task<OkObjectResult> Logout(LogoutViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.Identifiers);
        if (user == null)
            return Ok(new
            {
                Code = 400,
                data = false
            });

        user.RefreshToken           = string.Empty;
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(-10);
        _accountRepository.Update(user);
        _accountRepository.SaveChanges();
        return Ok(new
        {
            Code = 200,
            data = true
        });
    }

    /// <summary>
    ///     GenerateJwt
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    private async Task<string> GenerateJwt(string userId)
    {
        var user           = await _userManager.FindByIdAsync(userId);
        var identityClaims = new ClaimsIdentity();

        identityClaims.AddClaims(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id), 
            new Claim(CustomClaimType.UserId, user.Id)
        });
        identityClaims.AddClaim(new Claim(CustomClaimType.RefreshToken,
                                          string.IsNullOrEmpty(user.RefreshToken)
                                              ? string.Empty
                                              : user.RefreshToken));
        // =====Gán quyền sử dụng của User vào Token======
        var authorityByUserInfo = await _manageRoleQueries.GetAuthorityByUser(user.Id);
        if (authorityByUserInfo.Any())
        {
            identityClaims.AddClaim(new Claim("ATRT", authorityByUserInfo.FirstOrDefault()?.AuthorityId.ToString() ?? ""));
            identityClaims.AddClaim(new Claim("StaffName", authorityByUserInfo.FirstOrDefault()?.StaffName ?? ""));
            identityClaims.AddClaim(new Claim("StaffId", authorityByUserInfo.FirstOrDefault()?.StaffId.ToString() ?? ""));
            foreach (var items in authorityByUserInfo)
                identityClaims.AddClaim(new Claim(items.Code, items.Value.ToString()));
        }

        // =====Tạo Token=================================
        var tokenHandler = new JwtSecurityTokenHandler();
        var key          = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject  = identityClaims,
            Issuer   = _appSettings.Issuer,
            Audience = _appSettings.ValidAt,
            Expires  = DateTime.UtcNow.AddDays(_appSettings.Expiration),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key),
                                       SecurityAlgorithms.HmacSha256Signature)
        };

        return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
    }

    /// <summary>
        ///     Cấp lại mật khẩu
        /// </summary>
        /// <returns></returns>
        [HttpPost("reset-password")]
        public async Task<IActionResult> ReissuePassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (String.Compare(model.PassNew, model.PassNewRe, StringComparison.Ordinal) != 0)
                {
                    return NResponseBadRequest("Mật khẩu mới không giống nhau");
                }

                var iUser = _accountRepository.GetById(model.UserId);
                if (iUser == null)
                {
                    // ModelState.AddModelError(string.Empty, "Không tìm thấy tài khoản");
                    // return CustomResponse(ModelState);
                    return NResponseBadRequest("Không tìm thấy tài khoản");
                }

                //var iProject  = _projectManagerAppService.GetById(model.ProjectId);
                //var iUserName = !string.IsNullOrEmpty(iProject.Code) ? iProject.Code + "." + iUser.UserName : iUser.UserName;
                var account = _userManager.FindByNameAsync(iUser.UserName).Result;
                //await _userManager.FindByEmailAsync(model.Email);
                if (account == null)
                {
                    return NResponseBadRequest("Không tìm thấy tài khoản");
                }

                //Hash password
                // var passwordHash = new NCoreHelper().HashSha256(_configuration["SecretKey:Secret"], model.PassOld);
                // if (String.Compare(account.PasswordHash, passwordHash, StringComparison.Ordinal) != 0)
                // {
                //     ModelState.AddModelError(string.Empty, "Mật khẩu cũ không chính xác");
                //     return CustomResponse(ModelState);
                // }
                var iCheckPass = await _signInManager.PasswordSignInAsync(account, model.PassOld, false, true);
                if (iCheckPass.Succeeded)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(account);

                    var result = await _userManager.ResetPasswordAsync(account, token, model.PassNew);
                    if (result.Succeeded)
                        return Ok(new
                        {
                            success = true,
                            code    = 200,
                            data    = ""
                        });
                }

                return NResponseBadRequest("Mật khẩu cũ không chính xác");
            }

            return NResponseBadRequest("Lỗi chưa xử lý - Liên hệ ADMIN");
        }
#endregion
}