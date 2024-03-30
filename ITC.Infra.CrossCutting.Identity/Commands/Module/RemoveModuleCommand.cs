#region

using ITC.Infra.CrossCutting.Identity.Validations.Module;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Commands.Module;

public class RemoveModuleCommand : ModuleCommand
{
#region Constructors

    public RemoveModuleCommand(string id)
    {
        SetId(id);
    }

#endregion

#region Methods

    public override bool IsValid()
    {
        ValidationResult = new RemoveModuleCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

#endregion
}