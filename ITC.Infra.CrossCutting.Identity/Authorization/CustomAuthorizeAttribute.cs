#region

using Microsoft.AspNetCore.Authorization;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Authorization;

public class CustomAuthorizeAttribute : AuthorizeAttribute
{
#region Static Fields and Constants

    public const string POLICY_PREFIX = "ModuleIdentity";

#endregion

#region Fields

    private string _module;

#endregion

#region Constructors

    public CustomAuthorizeAttribute(string module, TypeAudit typeAudit)
    {
        TypeAudit = typeAudit;
        Module    = module;
    }

#endregion

#region Properties

    // Get or set the Age property by manipulating the underlying Policy property
    public string Module
    {
        get => _module;
        set
        {
            _module = value;
            Policy  = $"{POLICY_PREFIX}{value}.{(int)TypeAudit}";
        }
    }

    public TypeAudit TypeAudit { get; set; }

#endregion
}