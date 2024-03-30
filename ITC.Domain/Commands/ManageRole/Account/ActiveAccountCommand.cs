#region

using ITC.Domain.Validations.ManageRole.Account;

#endregion

namespace ITC.Domain.Commands.ManageRole.Account;

public class ActiveAccountCommand : AccountCommand
{
#region Constructors

    public ActiveAccountCommand(string id)
    {
        SetId(id);
    }

#endregion

#region Methods

    public override bool IsValid()
    {
        ValidationResult = new ActiveAccountCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

#endregion
}