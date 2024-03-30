#region

using System.Collections.Generic;
using ITC.Infra.CrossCutting.Identity.Authorization;
using ITC.Infra.CrossCutting.Identity.Validations.Module;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Commands.Module;

public class AddModuleCommand : ModuleCommand
{
#region Constructors

    public AddModuleCommand(string          name, string identity, string description, List<string> roleIds,
                            List<TypeAudit> weights)
    {
        Name        = name;
        Identity    = identity;
        Description = description;
        RoleIds     = roleIds;
        Weights     = weights;
    }

#endregion

#region Methods

    public override bool IsValid()
    {
        ValidationResult = new AddModuleCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

#endregion
}