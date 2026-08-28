using System.Text;
using System.Text.Json;
using Confluent.Kafka;
using RxFlow.Domain;

namespace RxFlow.Infrastructure;

public sealed class InventoryConnector(IHttpClientFactory clients) : IInventoryConnector
{
    public async Task<bool> AvailableAsync(string sku, CancellationToken cancellationToken)
    {
        try { return (await clients.CreateClient("inventory").GetAsync($"/frames/{sku}", cancellationToken)).IsSuccessStatusCode; }
        catch (HttpRequestException) { return true; }
    }
}

public sealed class LabConnector(IHttpClientFactory clients) : ILabConnector
{
    public async Task ScheduleAsync(string payload, CancellationToken cancellationToken)
    {
        for (var attempt = 0; attempt < 3; attempt++)
        {
            try { using var body = new StringContent(payload, Encoding.UTF8, "application/json"); _ = await clients.CreateClient("lab").PostAsync("/jobs", body, cancellationToken); return; }
            catch (TaskCanceledException) when (attempt < 2) { }
        }
    }
}

public sealed class KafkaEventPublisher(IProducer<string, string> producer) : IEventPublisher
{
    public Task PublishAsync(OrderSubmitted message, CancellationToken cancellationToken) => producer.ProduceAsync("rxflow.orders", new Message<string, string> { Key = message.OrderId.ToString(), Value = JsonSerializer.Serialize(message) }, cancellationToken);
}
