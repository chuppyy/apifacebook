#region

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Authorization;

public class CustomModulePolicyProvider : IAuthorizationPolicyProvider
{
#region Static Fields and Constants

    private const string POLICY_PREFIX = "ModuleIdentity";

#endregion

#region Constructors

    public CustomModulePolicyProvider(IOptions<AuthorizationOptions> options)
    {
        // ASP.NET Core only uses one authorization policy provider, so if the custom implementation
        // doesn't handle all policies (including default policies, etc.) it should fall back to an
        // alternate provider.
        //
        // In this sample, a default authorization policy provider (constructed with options from the 
        // dependency injection container) is used if this custom provider isn't able to handle a given
        // policy name.
        //
        // If a custom policy provider is able to handle all expected policy names then, of course, this
        // fallback pattern is unnecessary.
        FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
    }

#endregion

#region Properties

    public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

#endregion

#region IAuthorizationPolicyProvider Members

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
    {
        return FallbackPolicyProvider.GetDefaultPolicyAsync();
    }

    public Task<AuthorizationPolicy> GetFallbackPolicyAsync()
    {
        return FallbackPolicyProvider.GetFallbackPolicyAsync();
    }

    // Policies are looked up by string name, so expect 'parameters' (like age)
    // to be embedded in the policy names. This is abstracted away from developers
    // by the more strongly-typed attributes derived from AuthorizeAttribute
    // (like [MinimumAgeAuthorize] in this sample)
    public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
    {
        if (policyName.StartsWith(POLICY_PREFIX, StringComparison.OrdinalIgnoreCase))
        {
            var param           = policyName.Substring(POLICY_PREFIX.Length);
            var subStringPolicy = param.Split(new[] { '.' });
            if (subStringPolicy.Length > 1 && int.TryParse(subStringPolicy[1], out var typeAudit))
            {
                var moduleIdentity = subStringPolicy[0];
                var policy         = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new CustomRequirement(moduleIdentity, (TypeAudit)typeAudit));
                return Task.FromResult(policy.Build());
            }
        }

        // If the policy name doesn't match the format expected by this policy provider,
        // try the fallback provider. If no fallback provider is used, this would return 
        // Task.FromResult<AuthorizationPolicy>(null) instead.
        return FallbackPolicyProvider.GetPolicyAsync(policyName);
    }

#endregion
}