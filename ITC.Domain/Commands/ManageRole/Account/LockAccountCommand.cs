#region

using ITC.Domain.Validations.ManageRole.Account;

#endregion

namespace ITC.Domain.Commands.ManageRole.Account;

public class LockAccountCommand : AccountCommand
{
#region Constructors

    public LockAccountCommand(string id)
    {
        SetId(id);
    }

#endregion

#region Methods

    public override bool IsValid()
    {
        ValidationResult = new LockAccountCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

#endregion
}