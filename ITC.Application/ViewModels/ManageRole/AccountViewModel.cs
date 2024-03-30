#region

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

#endregion

namespace ITC.Application.ViewModels.ManageRole;

public class UserViewModel
{
#region Properties

    [Required(ErrorMessage = "The E-mail is Required")]
    [EmailAddress]
    [DisplayName("E-mail")]
    public string Email { get; set; }

    [Required(ErrorMessage = "The FullName is Required")]
    [MaxLength(255)]
    public string FullName { get; set; }

    public string Id { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                  MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [Required(ErrorMessage = "The PhoneNumber is Required")]
    [MaxLength(20)]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "The UserName is Required")]
    [MaxLength(255)]
    public string UserName { get; set; }

#endregion
}

public class AccountViewModel
{
#region Properties

    [MaxLength(50, ErrorMessage = "Địa chỉ không quá {1} ký tự")]
    public string Address { get; set; }

    public string    Avatar     { get; set; }
    public IFormFile AvatarFile { get; set; }

    [MaxLength(20, ErrorMessage = "Tỉnh thành phố không quá {1} ký tự")]
    public string City { get; set; }

    [MaxLength(50, ErrorMessage = "Quận/huyện không quá {1} ký tự")]
    public string District { get; set; }

    public int DistrictId { get; set; }

    //[Required(ErrorMessage = "Email không được để trống")]
    [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
    [DisplayName("E-mail")]
    public string Email { get; set; }

    public bool EmailConfirmed { get; set; }

    /// <summary>
    ///     Thông tin lỗi
    ///     Dùng cho nạp từ tập tin
    /// </summary>
    public string ErrorMessage { get; set; }

    [Phone(ErrorMessage = "Fax không đúng định dạng")]
    [MaxLength(20, ErrorMessage = "Fax không quá 20 ký tự")]
    public string Fax { get; set; }

    public IFormFile File { get; set; }

    [Required(ErrorMessage = "Tên không được để trống")]
    [MaxLength(255, ErrorMessage = "Tên không quá 255 ký tự")]
    //[RegularExpression(@"^[^_\\.,\\(\\)^%$#@!\\*]+$", ErrorMessage = "Vui lòng không nhập kí tự đặc biệt")]
    public string FullName { get; set; }

    /// <summary>
    ///     Tồn tại lỗi
    ///     Dùng cho nạp từ tập tin
    /// </summary>
    public bool HasError { get; set; }

    public string          Id         { get; set; }
    public DateTimeOffset? LockoutEnd { get; set; }


    [Required(ErrorMessage = "Mật khẩu không được để trống!")]
    [StringLength(100, ErrorMessage = "Mật khẩu phải ít nhất {2} và dài nhất {1} ký tự.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }


    [Phone(ErrorMessage = "Số điện thoại không đúng định dạng")]
    [MaxLength(20, ErrorMessage = "Số điện thoại không quá 20 ký tự")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "The PortalId is Required")]
    public int PortalId { get; set; }

    public int    ProvinceId { get; set; }
    public string Role       { get; set; }
    public int    UserGroup  { get; set; }

    [Required(ErrorMessage = "Tên đăng nhập không được để trống!")]
    [MaxLength(255, ErrorMessage = "Tên đăng nhập không dài quá 255 ký tự")]
    public string UserName { get; set; }


    public string UserTypeId { get; set; }

    public string UserTypeName { get; set; }

    [MaxLength(50, ErrorMessage = "Xã/phường không quá {1} ký tự")]
    public string Ward { get; set; }

    public int    WardId  { get; set; }
    public string Website { get; set; }

#endregion
}

public class AccountEditViewModel
{
#region Properties

    public string    Avatar     { get; set; }
    public IFormFile AvatarFile { get; set; }


    [Required(ErrorMessage = "Email không được để trống")]
    [EmailAddress]
    [DisplayName("E-mail")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Tên không được để trống")]
    [MaxLength(255, ErrorMessage = "Tên không quá 255 ký tự")]
    public string FullName { get; set; }

    public string Id { get; set; }


    [MaxLength(20, ErrorMessage = "Số điện thoại không quá 20 ký tự")]
    public string PhoneNumber { get; set; }

    public int    PortalId { get; set; }
    public string UserName { get; set; }

    [Required(ErrorMessage = "The UserTypeId is Required")]
    public string UserTypeId { get; set; }

#endregion
}

public class AddAccountEmailViewModel
{
#region Properties

    [Required(ErrorMessage = "Email không được để trống")]
    [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
    [DisplayName("E-mail")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Id không được để trống")]
    public string Id { get; set; }

#endregion
}

public class ConfirmEmailViewModel
{
#region Constructors

    public ConfirmEmailViewModel(string userId, string code)
    {
        UserId = userId;
        Code   = code;
    }

    public ConfirmEmailViewModel()
    {
    }

#endregion

#region Properties

    public string Code { get; set; }

    [Required(ErrorMessage = "Id không được để trống")]
    public string UserId { get; set; }

#endregion
}

public class EditPersonnelAccountViewModel : AccountEditViewModel
{
#region Properties

    public string Address { get; set; }

#endregion
}