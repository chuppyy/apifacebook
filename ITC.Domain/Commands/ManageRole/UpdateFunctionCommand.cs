#region

using ITC.Domain.Validations.ManageRole;

#endregion

namespace ITC.Domain.Commands.ManageRole;

public class UpdateFunctionCommand : FunctionCommand
{
#region Constructors

    public UpdateFunctionCommand(string name, int weight, string description, string moduleId) :
        base(name, weight, description, moduleId)
    {
    }

#endregion

#region Methods

    public override bool IsValid()
    {
        ValidationResult = new UpdateFunctionCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

#endregion
}