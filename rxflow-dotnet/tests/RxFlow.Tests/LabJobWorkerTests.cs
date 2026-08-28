using Confluent.Kafka;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using RxFlow.Domain;
using RxFlow.Workers;

namespace RxFlow.Tests;

public sealed class LabJobWorkerTests
{
    [Fact]
    public async Task PublishesScheduledJob()
    {
        var id = Guid.NewGuid(); var repository = Substitute.For<IOrderRepository>(); var connector = Substitute.For<ILabConnector>(); var events = Substitute.For<IEventPublisher>();
        repository.FindAsync(id, Arg.Any<CancellationToken>()).Returns(new Order { Id = id, PatientId = "SYN-2", FrameSku = "F-2", Sphere = 1m, Cylinder = 0m, Axis = 90, Material = "STANDARD", Coating = "NONE", Price = 10m, Status = "RECEIVED", LabCode = "LAB-EAST", SubmittedAt = DateTimeOffset.UtcNow });
        await new LabJobWorker(repository, connector, events, NullLogger<LabJobWorker>.Instance).ProcessAsync(id, CancellationToken.None);
        await connector.Received(1).ScheduleAsync(Arg.Any<string>(), Arg.Any<CancellationToken>());
        await events.Received(1).PublishAsync(Arg.Any<OrderSubmitted>(), Arg.Any<CancellationToken>());
    }
}
