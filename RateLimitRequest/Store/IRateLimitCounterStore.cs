using RateLimitRequest.Models;

namespace RateLimitRequest.Store;

public interface IRateLimitCounterStore : IRateLimitStore<RateLimitCounter?>
{
}