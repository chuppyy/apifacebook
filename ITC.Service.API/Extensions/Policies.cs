#region

using ITC.Infra.CrossCutting.Identity.Authorization;

#endregion

namespace ITC.Service.API.Extensions;

/// <summary>
/// </summary>
public static class Policies
{
#region Static Fields and Constants

    /// <summary>
    /// </summary>
    public const string Administrator = "Administrator";

#endregion

#region Methods

    /// <summary>
    /// </summary>
    /// <param name="moduleIdentity"></param>
    /// <param name="typeAudit"></param>
    /// <returns></returns>
    public static string GetPolicyName(string moduleIdentity, TypeAudit typeAudit)
    {
        return $"{CustomAuthorizeAttribute.POLICY_PREFIX}{moduleIdentity}.{(int)typeAudit}";
    }

#endregion
}