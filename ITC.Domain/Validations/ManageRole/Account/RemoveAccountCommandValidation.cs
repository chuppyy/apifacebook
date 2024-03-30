#region

using ITC.Domain.Commands.ManageRole.Account;

#endregion

namespace ITC.Domain.Validations.ManageRole.Account;

public class RemoveAccountCommandValidation : AccountValidation<RemoveAccountCommand>
{
#region Constructors

    public RemoveAccountCommandValidation()
    {
        ValidateId();
    }

#endregion
}