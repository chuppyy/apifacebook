#region

using ITC.Domain.Validations.ManageRole.Account;

#endregion

namespace ITC.Domain.Commands.ManageRole.Account;

public class UpdateAccountCommand : AccountCommand
{
#region Constructors

    public UpdateAccountCommand(string email, string fullName, string phoneNumber, string userTypeId, string avatar)
        : base(email, fullName, phoneNumber, userTypeId, avatar)
    {
    }

#endregion

#region Methods

    public override bool IsValid()
    {
        ValidationResult = new UpdateAccountCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

#endregion
}