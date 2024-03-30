#region

using FluentValidation;
using ITC.Domain.Commands.ManageRole.Account;

#endregion

namespace ITC.Domain.Validations.ManageRole.Account;

public class EducationDepartmentValidation<T> : AbstractValidator<T> where T : EducationDepartmentCommand
{
#region Methods

    protected void ValidateId()
    {
        RuleFor(c => c.Id)
            .NotEqual(string.Empty);
    }

#endregion
}