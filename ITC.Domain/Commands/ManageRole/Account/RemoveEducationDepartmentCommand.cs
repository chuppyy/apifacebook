#region

using System;
using ITC.Domain.Validations.ManageRole.Account;

#endregion

namespace ITC.Domain.Commands.ManageRole.Account;

public class RemoveEducationDepartmentCommand : EducationDepartmentCommand
{
#region Properties

    public Guid[] Ids { get; set; }

#endregion

#region Methods

    public override bool IsValid()
    {
        ValidationResult = new RemoveEducationDepartmentCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

#endregion

#region Constructors

    public RemoveEducationDepartmentCommand(string id)
    {
        SetId(id);
    }

    public RemoveEducationDepartmentCommand(Guid[] ids)
    {
        Ids = ids;
    }

#endregion
}