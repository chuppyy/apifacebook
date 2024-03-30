#region

using ITC.Domain.Validations.ManageRole.Account;

#endregion

namespace ITC.Domain.Commands.ManageRole.Account;

public class AddEducationDepartmentCommand : EducationDepartmentCommand
{
#region Constructors

    public AddEducationDepartmentCommand(string unitCode,   string identifier, string name,  string userName,
                                         string password,   string fullName,   string email, string phoneNumber,
                                         string userTypeId, string avatar,     int    portalId) : base(unitCode,
        identifier, name, userName, password, fullName, email, phoneNumber, userTypeId, avatar, portalId)
    {
    }

#endregion

#region Methods

    public override bool IsValid()
    {
        ValidationResult = new AddEducationDepartmentCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

#endregion
}