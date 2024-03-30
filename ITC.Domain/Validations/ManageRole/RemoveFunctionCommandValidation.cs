#region

using ITC.Domain.Commands.ManageRole;

#endregion

namespace ITC.Domain.Validations.ManageRole;

public class RemoveFunctionCommandValidation : FunctionValidation<RemoveFunctionCommand>
{
#region Constructors

    public RemoveFunctionCommandValidation()
    {
        ValidateId();
    }

#endregion
}