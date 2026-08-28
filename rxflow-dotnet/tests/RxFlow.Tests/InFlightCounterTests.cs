using FluentAssertions;
using NSubstitute;
using RxFlow.Application;
using StackExchange.Redis;

namespace RxFlow.Tests;

public sealed class InFlightCounterTests
{
    [Fact]
    public async Task IncrementsExistingCount()
    {
        var redis = Substitute.For<IConnectionMultiplexer>(); var database = Substitute.For<IDatabase>();
        redis.GetDatabase(Arg.Any<int>(), Arg.Any<object?>()).Returns(database);
        database.StringGetAsync("lab:LAB-EAST:inflight", Arg.Any<CommandFlags>()).Returns(new RedisValue("4"));
        var result = await new InFlightCounter(redis, new LegacyPriceEngine()).IncrementAsync("LAB-EAST");
        result.Should().Be(5);
        await database.Received().StringSetAsync("lab:LAB-EAST:inflight", (RedisValue)5L, null, false, When.Always, CommandFlags.None);
    }
}
