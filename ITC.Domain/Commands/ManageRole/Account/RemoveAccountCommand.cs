#region

using ITC.Domain.Validations.ManageRole.Account;

#endregion

namespace ITC.Domain.Commands.ManageRole.Account;

public class RemoveAccountCommand : AccountCommand
{
#region Properties

    public string[] Ids { get; set; }

#endregion

#region Methods

    public override bool IsValid()
    {
        ValidationResult = new RemoveAccountCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

#endregion

#region Constructors

    public RemoveAccountCommand(string id)
    {
        SetId(id);
    }

    public RemoveAccountCommand(string[] ids)
    {
        Ids = ids;
    }

#endregion
}