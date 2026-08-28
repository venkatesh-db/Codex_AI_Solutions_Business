namespace RxFlow.Domain;

public interface IOrderRepository
{
    Task AddAsync(Order order, CancellationToken cancellationToken);
    Task<Order?> FindAsync(Guid id, CancellationToken cancellationToken);
    Task SaveAsync(Order order, CancellationToken cancellationToken);
}

public interface ILabJobQueue { void Enqueue(Guid orderId); }
public interface ILabConnector { Task ScheduleAsync(string payload, CancellationToken cancellationToken); }
public interface IInventoryConnector { Task<bool> AvailableAsync(string sku, CancellationToken cancellationToken); }
public interface IEventPublisher { Task PublishAsync(OrderSubmitted message, CancellationToken cancellationToken); }
