#region

using ITC.Domain.Validations.ManageRole.Account;

#endregion

namespace ITC.Domain.Commands.ManageRole.Account;

public class AddDepartmentOfEducationCommand : DepartmentOfEducationCommand
{
#region Constructors

    public AddDepartmentOfEducationCommand(string code,       string identifier, string name, string userName,
                                           string password,   string fullName,   string email, string phoneNumber,
                                           string userTypeId, string avatar,     int portalId) : base(code, identifier,
        name, userName, password, fullName, email, phoneNumber, userTypeId, avatar, portalId)
    {
    }

#endregion

#region Methods

    public override bool IsValid()
    {
        ValidationResult = new AddDepartmentOfEducationCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

#endregion
}