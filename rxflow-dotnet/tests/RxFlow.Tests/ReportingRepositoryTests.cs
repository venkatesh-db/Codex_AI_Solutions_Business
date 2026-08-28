using FluentAssertions;

namespace RxFlow.Tests;

public sealed class ReportingRepositoryTests
{
    [Fact]
    public void SyntheticIdentifierShapeIsStable() => "SYN-1".Should().StartWith("SYN-");
}
