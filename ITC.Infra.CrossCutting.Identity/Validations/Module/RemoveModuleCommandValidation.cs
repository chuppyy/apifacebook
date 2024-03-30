#region

using ITC.Infra.CrossCutting.Identity.Commands.Module;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Validations.Module;

public class RemoveModuleCommandValidation : ModuleValidation<RemoveModuleCommand>
{
#region Constructors

    public RemoveModuleCommandValidation()
    {
        ValidateId();
    }

#endregion
}