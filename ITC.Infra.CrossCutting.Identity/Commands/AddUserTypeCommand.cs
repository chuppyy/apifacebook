#region

using System.Collections.Generic;
using ITC.Infra.CrossCutting.Identity.Models.QueryModel;
using ITC.Infra.CrossCutting.Identity.Validations;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Commands;

public class AddUserTypeCommand : UserTypeCommand
{
#region Constructors

    public AddUserTypeCommand(string        name, string description, string roleId, string selectedItems,
                              List<RoleDTO> roles) : base(name, description, roleId, selectedItems, roles)
    {
    }

#endregion

#region Methods

    public override bool IsValid()
    {
        ValidationResult = new AddUserTypeCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

#endregion
}

public class AddUserTypeUnitCommand : AddUserTypeCommand
{
#region Constructors

    public AddUserTypeUnitCommand(string        name, string description, string roleId, string selectedItems,
                                  List<RoleDTO> roles) : base(name, description, roleId, selectedItems, roles)
    {
    }

#endregion

#region Methods

    public override bool IsValid()
    {
        ValidationResult = new AddUserTypeCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

#endregion
}