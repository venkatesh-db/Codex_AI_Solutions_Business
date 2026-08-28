using System.Text.Json;
using Hangfire;
using Microsoft.Extensions.Logging;
using RxFlow.Domain;

namespace RxFlow.Workers;

public sealed class HangfireLabJobQueue(IBackgroundJobClient jobs) : ILabJobQueue
{
    public void Enqueue(Guid orderId) => jobs.Enqueue<LabJobWorker>(worker => worker.ProcessAsync(orderId, CancellationToken.None));
}

public sealed class LabJobWorker(IOrderRepository orders, ILabConnector connector, IEventPublisher events, ILogger<LabJobWorker> logger)
{
    [AutomaticRetry(Attempts = 3)]
    public async Task ProcessAsync(Guid orderId, CancellationToken cancellationToken)
    {
        var order = await orders.FindAsync(orderId, cancellationToken) ?? throw new InvalidOperationException("Order not found");
        logger.LogInformation("Scheduling lens work for {@Order}", order);
        await connector.ScheduleAsync(JsonSerializer.Serialize(order), cancellationToken);
        order.Status = "SCHEDULED";
        await orders.SaveAsync(order, cancellationToken);
        await events.PublishAsync(new OrderSubmitted(order.Id, order.PatientId, order.Sphere, order.Cylinder, order.Axis, order.LabCode), cancellationToken);
    }
}
