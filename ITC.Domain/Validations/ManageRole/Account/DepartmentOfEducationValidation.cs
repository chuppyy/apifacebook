#region

using FluentValidation;
using ITC.Domain.Commands.ManageRole.Account;

#endregion

namespace ITC.Domain.Validations.ManageRole.Account;

public class DepartmentOfEducationValidation<T> : AbstractValidator<T> where T : DepartmentOfEducationCommand
{
#region Methods

    protected void ValidateId()
    {
        RuleFor(c => c.Id)
            .NotEqual(string.Empty);
    }

#endregion
}