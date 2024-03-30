#region

using ITC.Domain.Commands.ManageRole.Account;

#endregion

namespace ITC.Domain.Validations.ManageRole.Account;

public class LockAccountCommandValidation : AccountValidation<LockAccountCommand>
{
#region Constructors

    public LockAccountCommandValidation()
    {
        ValidateId();
    }

#endregion
}