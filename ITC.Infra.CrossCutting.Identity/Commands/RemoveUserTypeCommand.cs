#region

using ITC.Infra.CrossCutting.Identity.Validations;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Commands;

public class RemoveUserTypeCommand : UserTypeCommand
{
#region Methods

    public override bool IsValid()
    {
        ValidationResult = new RemoveUserTypeCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

#endregion

#region Constructors

    public RemoveUserTypeCommand(string id, string userTypeNewId)
    {
        SetId(id);
        UserTypeNewId = userTypeNewId;
    }

    public RemoveUserTypeCommand(string id, string userTypeNewId, bool isUnit)
    {
        SetId(id);
        UserTypeNewId = userTypeNewId;
        IsUnit        = isUnit;
    }

#endregion

#region Properties

    public bool   IsUnit        { get; }
    public string UserTypeNewId { get; }

#endregion
}