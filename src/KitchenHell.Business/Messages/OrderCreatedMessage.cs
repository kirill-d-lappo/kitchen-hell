using KitchenHell.Messaging.Producers;

namespace KitchenHell.Business.Messages;

[MessagingProducer(typeof(string))]
public class OrderCreatedMessage
{
    public long OrderId { get; set; }

    public long RestaurantId { get; set; }
}
