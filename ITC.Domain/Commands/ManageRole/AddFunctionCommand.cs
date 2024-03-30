#region

using ITC.Domain.Validations.ManageRole;

#endregion

namespace ITC.Domain.Commands.ManageRole;

public class AddFunctionCommand : FunctionCommand
{
#region Constructors

    public AddFunctionCommand(string name, int weight, string description, string moduleId) :
        base(name, weight, description, moduleId)
    {
    }

#endregion

#region Methods

    public override bool IsValid()
    {
        ValidationResult = new AddFunctionCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

#endregion
}