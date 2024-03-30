#region

using ITC.Infra.CrossCutting.Identity.Validations.Portal;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Commands.Portal;

public class RemovePortalCommand : PortalCommand
{
#region Constructors

    public RemovePortalCommand(string identity)
    {
        Identity = identity;
        SetAggregateId(identity);
    }

#endregion

#region Methods

    public override bool IsValid()
    {
        ValidationResult = new RemovePortalCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

#endregion
}