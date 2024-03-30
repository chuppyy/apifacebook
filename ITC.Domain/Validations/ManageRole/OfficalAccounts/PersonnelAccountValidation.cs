#region

using FluentValidation;
using ITC.Domain.Commands.ManageRole.OfficalAccount;

#endregion

namespace ITC.Domain.Validations.ManageRole.OfficalAccounts;

public class PersonnelAccountValidation<T> : AbstractValidator<T> where T : PersonnelAccountCommand
{
#region Methods

    protected void Staff()
    {
        RuleFor(c => c.StaffRecords.Count).LessThan(1).WithMessage("Chọn ít nhất 1 cán bộ");
    }

    protected void ValidateId()
    {
        RuleFor(c => c.Id)
            .NotEqual(string.Empty);
    }

#endregion
}