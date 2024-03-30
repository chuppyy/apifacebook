#region

using FluentValidation;
using ITC.Infra.CrossCutting.Identity.Commands.Portal;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Validations.Portal;

public class PortalValidation<T> : AbstractValidator<T> where T : PortalCommand
{
#region Methods

    protected void ValidateId()
    {
        RuleFor(c => c.Id)
            .NotNull();
    }

#endregion
}