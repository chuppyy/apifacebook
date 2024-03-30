#region

using FluentValidation;
using ITC.Domain.Commands.ManageRole.Account;

#endregion

namespace ITC.Domain.Validations.ManageRole.Account;

public class AddEmailAccountCommandValidation : AccountValidation<AddEmailAccountCommand>
{
#region Constructors

    public AddEmailAccountCommandValidation()
    {
        RuleFor(x => x.Email).EmailAddress().WithMessage("không đúng định dạng email")
                             .NotEqual(string.Empty).WithMessage("email không để trống")
                             .NotNull().WithMessage("email không để trống");
    }

#endregion
}