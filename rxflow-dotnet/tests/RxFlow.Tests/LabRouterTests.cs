using FluentAssertions;
using RxFlow.Application;

namespace RxFlow.Tests;

public sealed class LabRouterTests
{
    [Fact]
    public void SelectsConfiguredLab() => new LabRouter().Choose("POLYCARBONATE", "AR").Code.Should().Be("LAB-EAST");
}
