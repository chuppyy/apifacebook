#region

using System.Threading.Tasks;
using RateLimitRequest.Models;

#endregion

namespace RateLimitRequest.Store;

public interface IClientPolicyStore : IRateLimitStore<ClientRateLimitPolicy>
{
#region Methods

    Task SeedAsync();

#endregion
}