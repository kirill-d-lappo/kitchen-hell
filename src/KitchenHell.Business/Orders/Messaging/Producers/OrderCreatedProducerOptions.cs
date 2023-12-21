namespace KitchenHell.Business.Orders.Messaging.Producers;

public sealed record OrderCreatedProducerOptions
{
    public const string Name = "OrderCreated";

    public string TopicName { get; init; }

    public bool IsEnabled { get; set; }
}