#region

using ITC.Domain.Validations.ManageRole.Account;

#endregion

namespace ITC.Domain.Commands.ManageRole.Account;

public class UnlockAccountCommand : AccountCommand
{
#region Constructors

    public UnlockAccountCommand(string id)
    {
        SetId(id);
    }

#endregion

#region Methods

    public override bool IsValid()
    {
        ValidationResult = new UnlockAccountCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

#endregion
}