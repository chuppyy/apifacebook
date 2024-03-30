#region

using FluentValidation;
using ITC.Domain.Commands.ManageRole.OfficalAccount;

#endregion

namespace ITC.Domain.Validations.ManageRole.OfficalAccounts;

public class AccountInforCommandValidation : AbstractValidator<AccountInfoCommand>
{
#region Constructors

    public AccountInforCommandValidation()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Tên không để trống");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Email không đúng định dạng")
                             .NotEmpty().WithMessage("Email không để trống")
                             .MaximumLength(50).WithMessage("Email không quá 50 ký tự");
        RuleFor(x => x.PhoneNumber).MaximumLength(20).WithMessage("Số điện thoại không để trống");
    }

#endregion
}