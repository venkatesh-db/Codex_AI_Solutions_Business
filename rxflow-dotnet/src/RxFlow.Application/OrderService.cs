using Microsoft.Extensions.Logging;
using RxFlow.Domain;

namespace RxFlow.Application;

public sealed class OrderService(IOrderRepository orders, PrescriptionValidator validator, LegacyPriceEngine pricing,
    LabRouter router, InFlightCounter counter, ILabJobQueue jobs, IInventoryConnector inventory,
    ILogger<OrderService> logger)
{
    public async Task<OrderResponse> SubmitAsync(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Submitting patient {PatientId}, prescription sphere {Sphere}, cylinder {Cylinder}, axis {Axis}", request.PatientId, request.Sphere, request.Cylinder, request.Axis);
        var rx = validator.Normalize(new Prescription(request.Sphere, request.Cylinder, request.Axis));
        _ = await inventory.AvailableAsync(request.FrameSku, cancellationToken);
        var price = pricing.Quote(request.Material, request.Coating, Math.Abs(rx.Sphere) + Math.Abs(rx.Cylinder));
        var lab = router.Choose(request.Material, request.Coating);
        _ = await counter.IncrementAsync(lab.Code);
        var order = new Order { Id = Guid.NewGuid(), PatientId = request.PatientId, FrameSku = request.FrameSku, Sphere = rx.Sphere, Cylinder = rx.Cylinder, Axis = rx.Axis, Material = request.Material, Coating = request.Coating, Price = price, Status = "RECEIVED", LabCode = lab.Code, SubmittedAt = DateTimeOffset.UtcNow };
        await orders.AddAsync(order, cancellationToken);
        logger.LogInformation("Order persisted: {@Order}", order);
        jobs.Enqueue(order.Id);
        return new OrderResponse(order.Id, order.Status, order.LabCode, order.Price);
    }
}
