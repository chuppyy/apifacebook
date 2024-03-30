#region

using ITC.Domain.Commands.ManageRole.Account;

#endregion

namespace ITC.Domain.Validations.ManageRole.Account;

public class DisableAccountCommandValidation : AccountValidation<DisableAccountCommand>
{
#region Constructors

    public DisableAccountCommandValidation()
    {
        ValidateId();
    }

#endregion
}