#region

using System.Collections.Generic;
using ITC.Infra.CrossCutting.Identity.Authorization;
using ITC.Infra.CrossCutting.Identity.Validations.Module;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Commands.Module;

public class UpdateModuleCommand : ModuleCommand
{
#region Constructors

    public UpdateModuleCommand(string name,    string          identity, string description, List<string> roleIds,
                               string groupId, List<TypeAudit> weights)
    {
        Name        = name;
        Identity    = identity;
        Description = description;
        RoleIds     = roleIds;
        Weights     = weights;
        GroupId     = groupId;
    }

#endregion

#region Methods

    public override bool IsValid()
    {
        ValidationResult = new UpdateModuleCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

#endregion
}