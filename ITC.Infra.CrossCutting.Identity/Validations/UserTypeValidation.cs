#region

using FluentValidation;
using ITC.Infra.CrossCutting.Identity.Commands;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Validations;

public class UserTypeValidation<T> : AbstractValidator<T> where T : UserTypeCommand
{
#region Methods

    protected void ValidateId()
    {
        RuleFor(c => c.Id)
            .NotEqual(string.Empty);
    }

#endregion
}