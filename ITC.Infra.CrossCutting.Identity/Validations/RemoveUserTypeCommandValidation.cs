#region

using ITC.Infra.CrossCutting.Identity.Commands;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Validations;

public class RemoveUserTypeCommandValidation : UserTypeValidation<RemoveUserTypeCommand>
{
#region Constructors

    public RemoveUserTypeCommandValidation()
    {
        ValidateId();
    }

#endregion
}