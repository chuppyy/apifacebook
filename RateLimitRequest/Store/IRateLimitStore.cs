#region

using System;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace RateLimitRequest.Store;

public interface IRateLimitStore<T>
{
#region Methods

    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
    Task<T>    GetAsync(string    id, CancellationToken cancellationToken = default);
    Task       RemoveAsync(string id, CancellationToken cancellationToken = default);

    Task SetAsync(string            id, T entry, TimeSpan? expirationTime = null,
                  CancellationToken cancellationToken = default);

#endregion
}