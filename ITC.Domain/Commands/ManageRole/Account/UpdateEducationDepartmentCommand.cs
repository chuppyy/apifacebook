#region

using ITC.Domain.Validations.ManageRole.Account;

#endregion

namespace ITC.Domain.Commands.ManageRole.Account;

public class UpdateEducationDepartmentCommand : EducationDepartmentCommand
{
#region Methods

    public override bool IsValid()
    {
        ValidationResult = new UpdateEducationDepartmentCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

#endregion

#region Constructors

    public UpdateEducationDepartmentCommand()
    {
    }

    public UpdateEducationDepartmentCommand(string name,       string email, string fullName, string phoneNumber,
                                            string userTypeId, string avatar) : base(name, email, fullName,
        phoneNumber, userTypeId, avatar)
    {
    }

#endregion
}