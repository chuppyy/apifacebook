#region

using System.Threading.Tasks;
using RateLimitRequest.Models;

#endregion

namespace RateLimitRequest.Store;

public interface IIpPolicyStore : IRateLimitStore<IpRateLimitPolicies>
{
#region Methods

    Task SeedAsync();

#endregion
}