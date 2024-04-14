using KitchenHell.Messaging;

namespace KitchenHell.Business.Messages;

public class OrderCreatedMessage : IMessage<string>
{
  public long OrderId { get; set; }

  public long RestaurantId { get; set; }

  public DateTimeOffset Timestamp { get; set; }

  public string GetKey()
  {
    return OrderId.ToString();
  }
}
