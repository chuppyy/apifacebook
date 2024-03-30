#region

using FluentValidation;
using ITC.Domain.Commands.ManageRole.OfficalAccount;

#endregion

namespace ITC.Domain.Validations.ManageRole.OfficalAccounts;

public class RemovePersonnelAccountCommandValidation : PersonnelAccountValidation<RemovePersonnelAccountCommand>
{
#region Constructors

    public RemovePersonnelAccountCommandValidation()
    {
        RuleFor(c => c.Ids.Length)
            .NotNull().WithMessage("Id không để trống");
    }

#endregion
}