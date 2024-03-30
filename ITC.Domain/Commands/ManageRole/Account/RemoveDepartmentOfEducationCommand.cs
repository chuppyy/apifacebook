#region

using ITC.Domain.Validations.ManageRole.Account;

#endregion

namespace ITC.Domain.Commands.ManageRole.Account;

public class RemoveDepartmentOfEducationCommand : DepartmentOfEducationCommand
{
#region Constructors

    public RemoveDepartmentOfEducationCommand(string id)
    {
        SetId(id);
    }

#endregion

#region Methods

    public override bool IsValid()
    {
        ValidationResult = new RemoveDepartmentOfEducationCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

#endregion
}