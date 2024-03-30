#region

using ITC.Domain.Commands.ManageRole.Account;

#endregion

namespace ITC.Domain.Validations.ManageRole.Account;

public class UnlockAccountCommandValidation : AccountValidation<UnlockAccountCommand>
{
#region Constructors

    public UnlockAccountCommandValidation()
    {
        ValidateId();
    }

#endregion
}