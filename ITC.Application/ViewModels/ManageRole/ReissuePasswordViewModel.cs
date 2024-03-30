#region

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ITC.Application.ViewModels.ManageRole;

public class ReissuePasswordViewModel
{
#region Properties

    [Required(ErrorMessage = "Xác thực mật khẩu không được để trống.")]
    [StringLength(100, ErrorMessage = "Mật khẩu phải ít nhất {2} và dài nhất {1} ký tự.", MinimumLength = 6)]
    [Compare("NewPassword", ErrorMessage = "Xác thực mật khẩu không đúng.")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string ConfirmNewPassword { get; set; }

    [Required(ErrorMessage = "Mật khẩu mới không được để trống.")]
    [StringLength(100, ErrorMessage = "Mật khẩu phải ít nhất {2} và dài nhất {1} ký tự.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string NewPassword { get; set; }

    public string UserId { get; set; }

#endregion
}

public class ResetPasswordViewModel
{
#region Properties

    public string PassOld   { get; set; }
    public string PassNew   { get; set; }
    public string PassNewRe { get; set; }
    public string UserId    { get; set; }
    public Guid   ProjectId { get; set; }

#endregion
}