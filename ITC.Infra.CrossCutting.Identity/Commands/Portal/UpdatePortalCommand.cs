#region

using ITC.Infra.CrossCutting.Identity.Validations.Portal;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Commands.Portal;

public class UpdatePortalCommand : PortalCommand
{
#region Constructors

    public UpdatePortalCommand(string name) : base(name)
    {
    }

#endregion

#region Methods

    public override bool IsValid()
    {
        ValidationResult = new UpdatePortalCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

#endregion
}