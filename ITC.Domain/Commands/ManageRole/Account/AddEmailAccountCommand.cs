#region

using ITC.Domain.Validations.ManageRole.Account;

#endregion

namespace ITC.Domain.Commands.ManageRole.Account;

public class AddEmailAccountCommand : AccountCommand
{
#region Methods

    public override bool IsValid()
    {
        ValidationResult = new AddEmailAccountCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

#endregion
}