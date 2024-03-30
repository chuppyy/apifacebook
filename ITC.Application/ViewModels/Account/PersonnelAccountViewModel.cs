#region

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ITC.Application.CustomValidations;

#endregion

namespace ITC.Application.ViewModels.Account;

public class PersonnelAccountViewModel
{
#region Properties

    [Required(ErrorMessage = "Mật khẩu không được để trống")]
    [Display(Name = "Confirm password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Xác thực mật khẩu không đúng.")]
    public string ConfirmPassword { get; set; }

    public string Description { get; set; }
    public string Id          { get; set; }

    public bool IsAutoGenUserName { get; set; }

    [Required(ErrorMessage = "Mật khẩu không được để trống")]
    [StringLength(100, ErrorMessage = "Mật khẩu phải ít nhất {2} và nhiều ít {1} ký tự.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [EnsureOneElement(ErrorMessage = "Danh sách hồ sơ cán bộ rỗng")]
    public List<string> StaffRecords { get; set; }

    [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
    [MaxLength(255)]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Kiểu người dùng không được để trống")]
    public string UserTypeId { get; set; }

#endregion
}