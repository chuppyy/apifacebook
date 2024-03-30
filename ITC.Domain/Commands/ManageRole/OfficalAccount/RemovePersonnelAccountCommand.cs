#region

using ITC.Domain.Validations.ManageRole.OfficalAccounts;

#endregion

namespace ITC.Domain.Commands.ManageRole.OfficalAccount;

public class RemovePersonnelAccountCommand : PersonnelAccountCommand
{
#region Properties

    public string[] Ids { get; set; }

#endregion

#region Methods

    public override bool IsValid()
    {
        ValidationResult = new RemovePersonnelAccountCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

#endregion

#region Constructors

    public RemovePersonnelAccountCommand(string id)
    {
        SetId(id);
    }

    public RemovePersonnelAccountCommand(string[] ids)
    {
        Ids = ids;
    }

#endregion
}