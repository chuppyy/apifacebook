#region

using ITC.Domain.Commands.ManageRole.Account;
using ITC.Domain.Validations.ManageRole.OfficalAccounts;

#endregion

namespace ITC.Domain.Commands.ManageRole.OfficalAccount;

public class UpdatePersonnelAccountCommand : AccountCommand
{
#region Constructors

    public UpdatePersonnelAccountCommand(string email, string fullName, string phoneNumber, string userTypeId,
                                         string address) : base(email, fullName, phoneNumber, userTypeId, null)
    {
        Address = address;
    }

#endregion

#region Properties

    public string Address { get; set; }

#endregion

#region Methods

    public override bool IsValid()
    {
        ValidationResult = new UpdatePersonnelAccountCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

#endregion
}