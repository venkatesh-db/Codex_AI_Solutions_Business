using StackExchange.Redis;

namespace RxFlow.Infrastructure;

public sealed class FrameCache(IConnectionMultiplexer redis)
{
    public Task<RedisValue> GetAsync(string sku) => redis.GetDatabase().StringGetAsync($"frame:{sku}");
}
public sealed class JobLock(IConnectionMultiplexer redis)
{
    public Task<bool> AcquireAsync(Guid id) => redis.GetDatabase().StringSetAsync($"job-lock:{id}", "1", TimeSpan.FromMinutes(5), When.NotExists);
}
