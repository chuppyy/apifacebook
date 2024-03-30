#region

using ITC.Domain.Commands.ManageRole.Account;

#endregion

namespace ITC.Domain.Validations.ManageRole.Account;

public class ActiveAccountCommandValidation : AccountValidation<ActiveAccountCommand>
{
#region Constructors

    public ActiveAccountCommandValidation()
    {
        ValidateId();
    }

#endregion
}