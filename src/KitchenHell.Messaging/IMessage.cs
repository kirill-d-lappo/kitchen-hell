namespace KitchenHell.Messaging;

public interface IMessage<out TMessageKey>
{
  public TMessageKey GetKey();
}
