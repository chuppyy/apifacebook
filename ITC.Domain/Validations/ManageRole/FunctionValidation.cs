#region

using FluentValidation;
using ITC.Domain.Commands.ManageRole;

#endregion

namespace ITC.Domain.Validations.ManageRole;

public class FunctionValidation<T> : AbstractValidator<T> where T : FunctionCommand
{
#region Methods

    protected void ValidateId()
    {
        RuleFor(c => c.Id)
            .NotEqual(string.Empty);
    }

#endregion
}