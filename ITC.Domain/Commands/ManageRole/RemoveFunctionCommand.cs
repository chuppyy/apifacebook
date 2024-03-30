#region

using ITC.Domain.Validations.ManageRole;

#endregion

namespace ITC.Domain.Commands.ManageRole;

public class RemoveFunctionCommand : FunctionCommand
{
#region Constructors

    public RemoveFunctionCommand(string id)
    {
        SetId(id);
    }

#endregion

#region Methods

    public override bool IsValid()
    {
        ValidationResult = new RemoveFunctionCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

#endregion
}