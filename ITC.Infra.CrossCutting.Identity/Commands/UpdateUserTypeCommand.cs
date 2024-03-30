#region

using System.Collections.Generic;
using ITC.Infra.CrossCutting.Identity.Models.QueryModel;
using ITC.Infra.CrossCutting.Identity.Validations;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Commands;

public class UpdateUserTypeCommand : UserTypeCommand
{
#region Constructors

    public UpdateUserTypeCommand(string        name, string description, string roleId, string selectedItems,
                                 List<RoleDTO> roles) : base(name, description, roleId, selectedItems, roles)
    {
    }

#endregion

#region Methods

    public override bool IsValid()
    {
        ValidationResult = new UpdateUserTypeCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

#endregion
}

public class UpdateUserTypeUnitCommand : UpdateUserTypeCommand
{
#region Constructors

    public UpdateUserTypeUnitCommand(string        name, string description, string roleId, string selectedItems,
                                     List<RoleDTO> roles) : base(name, description, roleId, selectedItems, roles)
    {
    }

#endregion

#region Methods

    public override bool IsValid()
    {
        ValidationResult = new UpdateUserTypeUnitCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

#endregion
}