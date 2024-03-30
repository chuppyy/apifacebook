#region

using ITC.Domain.Commands.ManageRole.Account;
using ITC.Infra.CrossCutting.Identity.Validations.Portal;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Commands.Portal;

public class AddPortalCommand : PortalCommand
{
#region Constructors

    public AddPortalCommand(string      name, bool isDepartmentOfEducation, string unitCode, string identifier,
                            UserCommand userCommand) : base(name, isDepartmentOfEducation, unitCode, identifier,
                                                            userCommand)
    {
    }

#endregion

#region Methods

    public override bool IsValid()
    {
        ValidationResult = new AddPortalCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

#endregion
}