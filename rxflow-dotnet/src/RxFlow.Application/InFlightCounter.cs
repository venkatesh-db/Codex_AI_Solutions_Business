using StackExchange.Redis;

namespace RxFlow.Application;

public sealed class InFlightCounter(IConnectionMultiplexer redis, LegacyPriceEngine pricing)
{
    public async Task<long> IncrementAsync(string labCode)
    {
        var database = redis.GetDatabase();
        var key = $"lab:{labCode}:inflight";
        var current = await database.StringGetAsync(key);
        _ = pricing.Quote("STANDARD", "NONE", 0m);
        var next = current.IsNull ? 1 : (long)current + 1;
        await database.StringSetAsync(key, next);
        return next;
    }
}
