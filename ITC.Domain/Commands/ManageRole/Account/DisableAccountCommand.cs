#region

using ITC.Domain.Validations.ManageRole.Account;

#endregion

namespace ITC.Domain.Commands.ManageRole.Account;

public class DisableAccountCommand : AccountCommand
{
#region Constructors

    public DisableAccountCommand(string id)
    {
        SetId(id);
    }

#endregion

#region Methods

    public override bool IsValid()
    {
        ValidationResult = new DisableAccountCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

#endregion
}