using FluentAssertions;
using RxFlow.Application;
using RxFlow.Domain;

namespace RxFlow.Tests;

public sealed class PrescriptionValidatorTests
{
    [Fact]
    public void AcceptsManufacturablePrescription()
    {
        var input = new Prescription(-2.25m, -1.25m, 92);
        new PrescriptionValidator().Normalize(input).Should().Be(input);
    }
}
