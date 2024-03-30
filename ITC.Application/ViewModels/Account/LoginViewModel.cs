#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ITC.Domain.Core.ModelShare.AuthorityManager;

#endregion

namespace ITC.Application.ViewModels.Account;

public class LoginViewModel
{
#region Properties

    // /// <summary>
    // /// Địa chỉ IP
    // /// </summary>
    // public string CIp { get; set; }

    /// <summary>
    ///     Mật khẩu
    /// </summary>
    [Required(ErrorMessage = "Mật khẩu không để trống")]
    [Display(Name = "Mật khẩu")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    /// <summary>
    ///     Nhớ lưu
    /// </summary>
    [Display(Name = "Lưu đăng nhập?")]
    public bool RememberMe { get; set; }

    /// <summary>
    ///     Tài khoản
    /// </summary>
    [Required(ErrorMessage = "Tên đăng nhập hoặc email không để trống")]
    [Display(Name = "Tên đăng nhập hoặc email")]
    public string UserName { get; set; }

    /// <summary>
    ///     domain
    /// </summary>
    public string UrlPage { get; set; }

#endregion
}

public class PortalData
{
    /// <summary>
    ///     Mã cổng
    /// </summary>
    public string Portal { get; set; }

    /// <summary>
    ///     Mã đơn vị
    /// </summary>
    public string Company { get; set; }

    /// <summary>
    ///     Tên đơn vị
    /// </summary>
    public string CompanyName { get; set; }

    /// <summary>
    ///     Mã quyền
    /// </summary>
    public Guid Authority { get; set; }

    /// <summary>
    ///     Danh sách menu
    /// </summary>
    public List<MenuRoleReturnViewModel> MenuRoles { get; set; }
}

/// <summary>
///     Dữ liệu người dùng
/// </summary>
public class aUserFile
{
    public string UserId      { get; set; }
    public string FullName    { get; set; }
    public string Avatar      { get; set; }
    public string Role        { get; set; }
    public string UrlHomePage { get; set; }
}