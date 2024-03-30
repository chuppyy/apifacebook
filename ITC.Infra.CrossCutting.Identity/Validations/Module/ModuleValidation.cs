#region

using FluentValidation;
using ITC.Infra.CrossCutting.Identity.Commands.Module;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Validations.Module;

public class ModuleValidation<T> : AbstractValidator<T> where T : ModuleCommand
{
#region Methods

    protected void ValidateId()
    {
        RuleFor(c => c.Id)
            .NotEqual(string.Empty);
    }

#endregion
}