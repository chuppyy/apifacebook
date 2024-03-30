#region

using System.Collections.Generic;
using ITC.Domain.Validations.ManageRole.OfficalAccounts;

#endregion

namespace ITC.Domain.Commands.ManageRole.OfficalAccount;

public class AddPersonnelAccountCommand : PersonnelAccountCommand
{
#region Constructors

    public AddPersonnelAccountCommand(string password, string userTypeId, List<string> staffRecords,
                                      string userName, bool   isAutogenUserName) : base(password, userTypeId,
        staffRecords, userName, isAutogenUserName)
    {
    }

#endregion

#region Methods

    public override bool IsValid()
    {
        ValidationResult = new AddPersonnelAccountCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

#endregion
}