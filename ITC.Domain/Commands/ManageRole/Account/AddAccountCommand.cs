#region

using ITC.Domain.Validations.ManageRole.Account;

#endregion

namespace ITC.Domain.Commands.ManageRole.Account;

public class AddAccountCommand : AccountCommand
{
#region Constructors

    public AddAccountCommand(string userName,   string password, string fullName, string email, string phoneNumber,
                             string userTypeId, string avatar) : base(userName, password, fullName, email,
                                                                      phoneNumber, userTypeId, avatar)
    {
    }

#endregion

#region Methods

    public override bool IsValid()
    {
        ValidationResult = new AddAccountCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

#endregion
}

public class AddUserCommand : UserCommand
{
#region Constructors

    public AddUserCommand(string userName, string password, string fullName, string email, string phoneNumber) :
        base(userName, password, fullName, email, phoneNumber)
    {
    }

#endregion

#region Methods

    public override bool IsValid()
    {
        ValidationResult = new AddUserCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

#endregion
}