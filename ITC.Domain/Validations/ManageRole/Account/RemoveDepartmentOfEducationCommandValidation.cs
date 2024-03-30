#region

using ITC.Domain.Commands.ManageRole.Account;

#endregion

namespace ITC.Domain.Validations.ManageRole.Account;

public class
    RemoveDepartmentOfEducationCommandValidation : DepartmentOfEducationValidation<
        RemoveDepartmentOfEducationCommand>
{
#region Constructors

    public RemoveDepartmentOfEducationCommandValidation()
    {
        ValidateId();
    }

#endregion
}