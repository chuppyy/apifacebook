#region

using System;
using ITC.Domain.Core.Commands;
using ITC.Domain.Validations.ManageRole.OfficalAccounts;

#endregion

namespace ITC.Domain.Commands.ManageRole.OfficalAccount;

public class AccountInfoCommand : Command
{
#region Methods

    public override bool IsValid()
    {
        ValidationResult = new AccountInforCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

#endregion

#region Properties

    /// <summary>
    ///     Địa chỉ
    /// </summary>
    public string Address { get; set; }

    public string Avatar { get; set; }

    public DateTime BirthDay { get; set; } = DateTime.Now;

    public string Code { get; set; }

    public string Email { get; set; }

    /// <summary>
    ///     Giới tính
    /// </summary>
    public bool Gender { get; set; }

    public Guid Id { get; set; }

    public bool IsSelected { get; set; }

    public string Management { get; set; }

    /// <summary>
    ///     Id Đơn vị quản lý
    /// </summary>
    public string ManagementId { get; set; }

    public string Name { get; set; }

    public string PhoneNumber { get; set; }

    public string Position { get; set; }

    /// <summary>
    ///     Id Chức vụ
    /// </summary>
    public string PositionId { get; set; }

    public string UserId { get; set; }

    /// <summary>
    ///     Đơn vị công tác
    /// </summary>
    public string Workplace { get; set; }

#endregion
}