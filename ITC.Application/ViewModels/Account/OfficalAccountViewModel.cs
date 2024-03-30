#region

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

#endregion

namespace ITC.Application.ViewModels.Account;

public class OfficalAccountViewModel
{
#region Properties

    public string    Address    { get; set; }
    public string    Avatar     { get; set; }
    public IFormFile AvatarFile { get; set; }

    public string City     { get; set; }
    public string Code     { get; set; }
    public string District { get; set; }

    [Required(ErrorMessage = "Email không được để trống")]
    [EmailAddress]
    [DisplayName("E-mail")]
    public string Email { get; set; }

    public bool EmailConfirmed { get; set; }

    /// <summary>
    ///     Thông tin lỗi
    ///     Dùng cho nạp từ tập tin
    /// </summary>
    public string ErrorMessage { get; set; }

    [MaxLength(20, ErrorMessage = "Fax không quá 20 ký tự")]
    public string Fax { get; set; }

    public IFormFile File { get; set; }

    [Required(ErrorMessage = "Tên không được để trống")]
    [MaxLength(255, ErrorMessage = "Tên không quá 255 ký tự")]
    public string FullName { get; set; }

    /// <summary>
    ///     Tồn tại lỗi
    ///     Dùng cho nạp từ tập tin
    /// </summary>
    public bool HasError { get; set; }

    public string Id { get; set; }


    [MaxLength(20, ErrorMessage = "Số điện thoại không quá 20 ký tự")]
    public string PhoneNumber { get; set; }

    public int    PortalId { get; set; }
    public string Position { get; set; }

    public string Role { get; set; }

    public string Status => EmailConfirmed ? "Kích hoạt" : "Chưa kích hoạt";

    public string UnitId     { get; set; }
    public string UnitUserId { get; set; }
    public int    UserGroup  { get; set; }
    public string UserId     { get; set; }
    public string UserName   { get; set; }
    public string UserType   { get; set; }

    public string UserTypeId { get; set; }
    public string Ward       { get; set; }
    public string Website    { get; set; }

#endregion
}