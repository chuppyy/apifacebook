#region

using Microsoft.AspNetCore.Authorization;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Authorization;

public class CustomRequirement : IAuthorizationRequirement
{
#region Constructors

    public CustomRequirement(string module, TypeAudit typeAudit)
    {
        Module    = module;
        TypeAudit = typeAudit;
    }

#endregion

#region Properties

    public string    Module    { get; set; }
    public TypeAudit TypeAudit { get; set; }

#endregion
}