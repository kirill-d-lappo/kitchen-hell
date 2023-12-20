using KitchenHell.Business.Messages;
using KitchenHell.Messaging.Producers;
using Microsoft.Extensions.Options;

namespace KitchenHell.Business.Orders.Messaging.Producers;

internal class OrderCreatedMessageProducer : IOrderCreatedMessageProducer
{
    private readonly IMessageProduceService<string, OrderCreatedMessage> _produceService;
    private readonly IOptionsMonitor<OrderCreatedProducerOptions> _optionsMonitor;

    public OrderCreatedMessageProducer(
        IMessageProduceService<string, OrderCreatedMessage> produceService,
        IOptionsMonitor<OrderCreatedProducerOptions> optionsMonitor
    )
    {
        _produceService = produceService;
        _optionsMonitor = optionsMonitor;
    }

    public async Task ProduceAsync(OrderCreatedMessage message, CancellationToken ct)
    {
        var config = _optionsMonitor.CurrentValue;
        if (!config.IsEnabled)
        {
            return;
        }

        var topic = config.TopicName;
        await _produceService.ProduceMessageAsync(message.OrderId.ToString(), message, topic, ct);
    }
}
