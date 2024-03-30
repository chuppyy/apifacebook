#region

using ITC.Domain.Validations.ManageRole.Account;

#endregion

namespace ITC.Domain.Commands.ManageRole.Account;

public class UpdateDepartmentOfEducationCommand : DepartmentOfEducationCommand
{
#region Constructors

    public UpdateDepartmentOfEducationCommand(string name,       string email, string fullName, string phoneNumber,
                                              string userTypeId, string avatar) : base(name, email, fullName,
        phoneNumber, userTypeId, avatar)
    {
    }

#endregion

#region Methods

    public override bool IsValid()
    {
        ValidationResult = new UpdateDepartmentOfEducationCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

#endregion
}