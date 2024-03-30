#region

using FluentValidation;
using ITC.Domain.Commands.ManageRole.Account;

#endregion

namespace ITC.Domain.Validations.ManageRole.Account;

public class AccountValidation<T> : UserValidation<T> where T : AccountCommand
{
}

public class UserValidation<T> : AbstractValidator<T> where T : UserCommand
{
#region Methods

    protected void ValidateId()
    {
        RuleFor(c => c.Id)
            .NotEqual(string.Empty);
    }

#endregion
}