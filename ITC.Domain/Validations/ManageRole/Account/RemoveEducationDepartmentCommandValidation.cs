#region

using ITC.Domain.Commands.ManageRole.Account;

#endregion

namespace ITC.Domain.Validations.ManageRole.Account;

public class
    RemoveEducationDepartmentCommandValidation : EducationDepartmentValidation<RemoveEducationDepartmentCommand>
{
#region Constructors

    public RemoveEducationDepartmentCommandValidation()
    {
        ValidateId();
    }

#endregion
}